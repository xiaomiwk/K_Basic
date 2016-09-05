using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace INET.会话
{
    class H队列监控
    {
        private long _总耗时;

        private long _总延时;

        private int _数量;

        private int _分组统计数量;

        private int _延迟阈值;

        private int _耗时阈值;

        public H队列监控(int __分组统计数量, int __延迟阈值, int __耗时阈值)
        {
            _分组统计数量 = __分组统计数量;
            _延迟阈值 = __延迟阈值;
            _耗时阈值 = __耗时阈值;
        }

        public void 监控下执行<T>(string __队列名称, T __数据, int __接收时间, Action<T> __处理数据)
        {
            var __延迟 = Environment.TickCount - __接收时间;
            Interlocked.Add(ref _总延时, __延迟);

            var __计时器 = new Stopwatch();
            __计时器.Start();
            __处理数据(__数据);
            var __耗时 = __计时器.ElapsedMilliseconds;

            Interlocked.Add(ref _总耗时, __耗时);
            Interlocked.Add(ref _数量, 1);
            if (_数量 >= _分组统计数量)
            {
                var __日志 = new StringBuilder();
                __日志.AppendFormat("处理数量 {0},", _数量);
                __日志.AppendFormat("总耗时 {0},", _总耗时);
                __日志.AppendFormat("平均处理耗时 {0} 毫秒,", _总耗时 / _数量);
                __日志.AppendFormat("平均延迟 {0},", _总延时 / _数量);
                H日志输出.记录("统计", __日志.ToString(), _总延时 / _数量 > _延迟阈值 ? TraceEventType.Warning : TraceEventType.Information);
                Interlocked.Exchange(ref _数量, 0);
                Interlocked.Exchange(ref _总耗时, 0);
                Interlocked.Exchange(ref _总延时, 0);
            }
            if (__耗时 > _耗时阈值)
            {
                var __日志 = new StringBuilder();
                __日志.AppendFormat("处理 {0} ,", __数据);
                __日志.AppendFormat("耗时 {0} 毫秒. ", __计时器.ElapsedMilliseconds);
                H日志输出.记录("耗时告警:" + __队列名称, __日志.ToString(), TraceEventType.Warning);
            }
            if (__延迟 > _延迟阈值)
            {
                var __日志 = new StringBuilder();
                __日志.AppendFormat("处理 {0} ,", __数据);
                __日志.AppendFormat("延迟 {0} 毫秒, ", __延迟);
                H日志输出.记录("延迟:" + __队列名称, __日志.ToString(), TraceEventType.Warning);
            }
        }

    }
}
