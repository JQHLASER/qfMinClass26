using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static qfmain.log日志;

namespace qfmain
{
    /// <summary>
    /// 目的是自动阻塞的线程 ,当 Queue中Add数据时,会触发
    /// </summary>
    public class Queue_触发队列<T>
    {
        public readonly BlockingCollection<T> _queue = new BlockingCollection<T>();     //Queue队列
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();   //令牌

        public void 初始化()
        {
            // 启动线程
            Task.Run(Queue处理, _cts.Token);
            _Isinistiall = true;
        }

        bool _Isinistiall = false;

        /// <summary>
        /// 添加到Queue队列
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            //添加到队列 
            this._queue.Add(t);
        }

        public void 释放()
        {
            if (!_Isinistiall)
            {
                return;
            }
            this._cts.Cancel();   //释放令牌
            this._queue.CompleteAdding();   //自动退出循环      会在消费完所有剩余数据后 自动退出 foreach 循环。
        }


        /// <summary>
        /// 处理队列,产生事件
        /// </summary> 
        private async Task Queue处理()
        {
            foreach (var s in _queue.GetConsumingEnumerable())
            {
                //处理事件
                this.Event_Queue?.Invoke(s); 
            }
        }

        /// <summary>
        /// 触发的处理事件
        /// </summary>
        public event Action<T> Event_Queue;

 


    }
}
