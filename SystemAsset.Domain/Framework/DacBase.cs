using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx;
using ArcheFx.Diagnostics;
using ArcheFx.EnterpriseServices;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using SystemAsset.Domain.Attributes;

namespace SystemAsset.Domain.Framework
{
    [
    ComponentType(ComponentType.DataAccess),
    TraceCategory("ArcheFx.DacBase")
    ]
    public abstract class DacBase : SqlDacBase
    {
        protected override void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage retMsg)
        {
            var attributes = callMsg.MethodBase.GetCustomAttributes(typeof(CacheAttribute), false);

            if (attributes.Length > 0)
            {
                CacheAttribute cacheAttribute = (CacheAttribute)attributes[0];

                //if (cacheAttribute.IsRefreshNeeded && !string.IsNullOrEmpty(cacheAttribute.CachedObjectName))
                //{
                //    //DB 에서 서버 IP 리스트 조회
                //    foreach (ServerT server in new ServerBiz().GetServerList())
                //    {
                //        Task.Factory.StartNew(() =>
                //            {
                //                //각각의 서버의 Cache를 갱신
                //                HttpWebRequest request = WebRequest.Create(
                //                    string.Format(@"http://{0}/ClientBin/HRMS-Web-Services-CacheService.svc/binary/RefreshCachedObject?cachedObjectName={1}",
                //                        server.ServerIP, cacheAttribute.CachedObjectName
                //                        )
                //                    ) as HttpWebRequest;
                //                request.Host = "hrms.ebay.co.kr";
                //                WebResponse response;
                //                try
                //                {
                //                    response = request.GetResponse();
                //                    //HRMSContext.RefreshCachedObject(cacheAttribute.CachedObjectName);
                //                }
                //                catch(Exception ex)
                //                {
                //                    HRMSContext.RefreshCachedObject(cacheAttribute.CachedObjectName);
                //                    //new ServerBiz().RemoveServer(server.ServerIP);
                //                }
                //            });
                //    }
                //}
            }
        }
    }
}
