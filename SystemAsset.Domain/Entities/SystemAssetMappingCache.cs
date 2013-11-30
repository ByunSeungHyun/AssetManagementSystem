using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;
using ArcheFx.Data;

namespace SystemAsset.Domain.Data
{
    public class SystemAssetMappingCache
    {
        private static Assembly _thisAssembly = null;
        private static HybridDictionary _dataMappingsHash = null;
        private const string RESOURCENAME_DATAMAPPINGS
            = "SystemAsset.Domain.Entities.AssetMapping.xml";

        static SystemAssetMappingCache()
        {
            _thisAssembly = Assembly.GetExecutingAssembly();
            _dataMappingsHash = new HybridDictionary();
        }

        // 논리적 데이터필드와 데이터베이스 테이블명, 필드명을 연결하기 위한 매핑테이블을 반환한다.
        // 리소스식별자로 어셈블리내의 XML 데이터를 로드, DataMappingCollection 클래스를 생성한 후 반환한다. 
        public static DataMappingCollection GetDataMappings(string mappingName)
        {
            if (mappingName == null && mappingName.Length == 0)
            {
                throw new ArgumentException("유효하지 않은 매핑이름입니다.",
                    "mappingName");
            }

            DataMappingCollection dataMappings = _dataMappingsHash[mappingName] as DataMappingCollection;

            if (dataMappings == null)
            {
                dataMappings = DataMappingCollection.CreateFromResourceStream(_thisAssembly, RESOURCENAME_DATAMAPPINGS, mappingName);
                _dataMappingsHash.Add(mappingName, dataMappings);
            }

            return dataMappings;
        }
    }
}
