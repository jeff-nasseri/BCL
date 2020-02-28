using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCL.Proxy
{
    class ConfigAction
    {
        //properties
        int good = 0, bad = 0, total = 0;
        List<string> Proxies { get; set; }
        int timeout { get; set; }
        int threadNumber { get; set; }
        string address { get; set; }
        CancellationToken CancellationToken { get; set; }



        /// <summary>
        /// set proxy to proxy list
        /// </summary>
        /// <param name="path">file path for load proxies</param>
        public void SetProxies(string path)
        {
            try
            {
                LooperQueries.SetProxies(path);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// write proxies to file
        /// </summary>
        /// <param name="path">file path</param>
        public void WriteProxies(string path)
        {
            try
            {
                LooperQueries.WriteProxies(path);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// remove proxy from proxies
        /// </summary>
        public void RemoveProxy(string proxy)
        {
            try
            {
                LooperQueries.RemoveProxy(proxy);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// initialization properties
        /// </summary>
        /// <param name="taskNumber">number of tasks for execute is alive function</param>
        /// <param name="address">domain name</param>
        /// <param name="timeout">request timeout</param>
        void initialization(string taskNumber, string address, string timeout)
        {
            this.Proxies = LooperQueries.GetProxies();
            this.threadNumber = Int32.Parse(taskNumber);
            this.timeout = Int32.Parse(timeout);
            this.address = address;
            this.total = Proxies.Count;
        }

        /// <summary>
        /// check connection validation for proxies
        /// </summary>
        public void CheckProxyConnection(string taskNumber, string address, string timeout)
        {
            initialization(taskNumber, address, timeout);

            var tokenSourcets = new CancellationTokenSource();
            CancellationToken = tokenSourcets.Token;

            for (int i = 0; i < Int32.Parse(taskNumber); i++)
            {
                Thread.Sleep(100);
                Task.Factory.StartNew(() => TaskManager(i), CancellationToken);
            }

            Console.ReadLine();
            tokenSourcets.Cancel();
        }

        /// <summary>
        /// check proxy and if proxy alive return true
        /// </summary>
        /// <param name="id">current thread id</param>
        void IsAlive(string ip, int port, int id)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.CreateHttp(address);
                req.Proxy = new WebProxy(ip, port);
                req.Timeout = timeout;
                var res = (HttpWebResponse)req.GetResponse();
                good++;
            }
            catch (Exception)
            {
                bad++;
                Proxies.Remove($"{ip}:{port}");
            }
            finally
            {
                CMD.ShowApplicationMessageToUser($"total proxies:{total}\tbad:{bad}\tgood:{good}\tip:{ip}:{port}\ttask id:{id}");
                id += threadNumber;
                TaskManager(id);
            }
        }

        /// <summary>
        /// manager for threads and send them for the status of being alive
        /// </summary>
        /// <param name="id">current id of thread</param>
        void TaskManager(int id)
        {
            while (id < Proxies.Count)
            {
                try
                {
                    if (CancellationToken.IsCancellationRequested)
                        break;

                    var proxy = Proxies[id];
                    var ip = proxy.Split(':')[0];
                    var port = Int32.Parse(proxy.Split(':')[1]);
                    IsAlive(ip, port, id);
                }
                catch (Exception) { }
            }
        }

    }
}
