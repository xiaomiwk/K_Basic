using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Utility.通用;

namespace Utility.任务
{
    public class H线程队列
    {
        private ConcurrentDictionary<string, M线程> _队列缓存 = new ConcurrentDictionary<string, M线程>();

        private H队列监控 _监控;

        /// <param name="__延迟阈值">毫秒</param>
        /// <param name="__耗时阈值">毫秒</param>
        public H线程队列(int __分组统计数量 = 1000, int __延迟阈值 = 3000, int __耗时阈值 = 100)
        {
            _监控 = new H队列监控(__分组统计数量, __延迟阈值, __耗时阈值);
        }

        public void 添加事项<T>(string __线程标识, string __队列标识, T __数据, Action<T> __处理数据, bool __监控 = false)
        {
            var __线程 = _队列缓存.GetOrAdd(__线程标识, k => new M线程(__线程标识));
            if (__监控)
            {
                __线程.添加事项(__队列标识, __数据, __处理数据, _监控);
            }
            else
            {
                __线程.添加事项(__队列标识, __数据, __处理数据);
            }
        }

        public void 关闭队列(string __线程标识)
        {
            M线程 __队列;
            if (_队列缓存.TryRemove(__线程标识, out __队列))
            {
                __队列.关闭();
            }
        }

        public void 关闭所有()
        {
            _队列缓存.Values.ToList().ForEach(q => q.关闭());
        }

        public void 取消队列(string __线程标识)
        {
            M线程 __队列;
            if (_队列缓存.TryRemove(__线程标识, out __队列))
            {
                __队列.取消();
            }
        }

        public void 取消所有()
        {
            _队列缓存.Values.ToList().ForEach(q => q.取消());
        }

        class M线程
        {
            private ConcurrentDictionary<string, ConcurrentQueue<Action>> _队列字典 = new ConcurrentDictionary<string, ConcurrentQueue<Action>>();

            private CancellationTokenSource _取消标志 = new CancellationTokenSource();

            private string _名称;

            private int _待处理数量;

            private bool _已关闭;

            private AutoResetEvent _同步信号 = new AutoResetEvent(false);

            public M线程(string __名称)
            {
                _名称 = __名称;

                new Thread(() =>
                {
                    while (!_取消标志.IsCancellationRequested)
                    {
                        var __所有队列 = _队列字典.Keys.ToList();
                        __所有队列.ForEach(__队列标识 =>
                        {
                            ConcurrentQueue<Action> __队列;
                            if (!_队列字典.TryGetValue(__队列标识, out __队列))
                            {
                                return;
                            }
                            Action __事项;
                            if (__队列.TryDequeue(out __事项))
                            {
                                Interlocked.Decrement(ref _待处理数量);
                                __事项();
                            }
                        });
                        if (_待处理数量 == 0)
                        {
                            if (_已关闭)
                            {
                                _同步信号.Set();
                                return;
                            }
                            Thread.Sleep(50);
                        }
                    }
                })
                { IsBackground = true }.Start();
            }

            public void 添加事项<T>(string __队列标识, T __数据, Action<T> __处理数据, H队列监控 __监控 = null)
            {
                if (_已关闭)
                {
                    return;
                }
                //Debug.WriteLine("{0} 添加事项 {1}", DateTime.Now.ToString("HH:mm:ss.fff"), __数据);
                var __接收时间 = Environment.TickCount;
                var __队列 = _队列字典.GetOrAdd(__队列标识, k => new ConcurrentQueue<Action>());
                __队列.Enqueue(() =>
                {
                    if (!_取消标志.IsCancellationRequested)
                    {
                        try
                        {
                            //Debug.WriteLine("{0} 执行事项 {1}", DateTime.Now.ToString("HH:mm:ss.fff"), __数据);
                            if (__监控 == null)
                            {
                                __处理数据(__数据);
                            }
                            else
                            {
                                __监控.监控下执行(_名称, __数据, __接收时间, __处理数据);
                            }
                        }
                        catch (Exception ex)
                        {
                            H日志.记录异常(ex, _名称);
                        }
                    }
                });
                Interlocked.Increment(ref _待处理数量);
            }

            public void 关闭()
            {
                _已关闭 = true;
                _同步信号.WaitOne();
            }

            public void 取消()
            {
                _取消标志.Cancel();
                _同步信号.Set();
            }
        }
    }

}
