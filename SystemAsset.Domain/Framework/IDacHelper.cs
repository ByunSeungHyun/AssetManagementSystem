using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ArcheFx.Data;

namespace SystemAsset.Domain.Framework
{
    /// <summary>
    /// DAC을 구현할 때 <c>SqlDbHelper</c>를 편리하게 사용하기 위한 방법을 제공합니다.
    /// </summary>
    public interface IDacHelper
    {
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫번째 엔티티 개체를 반환합니다. 나머지 엔티티 개체는 무시됩니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>첫번째 엔티티 개체</returns>
        object SelectSingleEntity(Type entityType, DataMappingCollection dataMapping, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫번째 엔티티 개체를 반환합니다. 나머지 엔티티 개체는 무시됩니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>첫번째 엔티티 개체</returns>
        object SelectSingleEntity(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 모든 엔티티 개체를 반환합니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 모든 엔티티 개체를 반환합니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 일부 영역의 엔티티 개체를 반환합니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="startIndex">부분적으로 반환될 엔티티 배열의 처음 위치에 대한 인덱스</param>
        /// <param name="length">부분적으로 반환될 엔티티 수</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, int startIndex, int length, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열 또는 결과 집합이 비어있을 경우 null 참조</returns>
        object SelectScalar(string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="dbNullDefault">반환된 값 형식이 <c>DBNull</c>일 경우 반환할 값</param>
        /// <param name="nullDefault">반환된 값이 null 참조일 경우 반환할 값</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열</returns>
        object SelectScalar(string query, object dbNullDefault, object nullDefault, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열 또는 결과 집합이 비어있을 경우 null 참조</returns>
        object SelectScalar(CommandType cmdType, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="dbNullDefault">반환된 값 형식이 <c>DBNull</c>일 경우 반환할 값</param>
        /// <param name="nullDefault">반환된 값이 null 참조일 경우 반환할 값</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열</returns>
        object SelectScalar(CommandType cmdType, string query, object dbNullDefault, object nullDefault, params SqlParameter[] sqlParams);
        /// <summary>
        /// 연결에 대한 Transact-SQL 문을 실행하고 영향을 받는 행의 수를 반환합니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>영향 받는 행의 수</returns>
        int Execute(string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 연결에 대한 Transact-SQL 문을 실행하고 영향을 받는 행의 수를 반환합니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>영향 받는 행의 수</returns>
        int Execute(CommandType cmdType, string query, params SqlParameter[] sqlParams);
        /// <summary>
        /// 현재 데이터베이스 서버의 현지 날짜와 시간인 DateTime을 가져옵니다.
        /// </summary>
        /// <returns>현재 날짜와 시간 값을 갖는 <c>DateTime</c></returns>
        DateTime GetDate();
        /// <summary>
        /// 이 컴퓨터의 현재 현지 날짜와 시간을 UTC로 표시한 DateTime을 가져옵니다.
        /// </summary>
        /// <returns>현재 UTC 날짜와 시간을 값으로 갖는 <c>DateTime</c></returns>
        DateTime GetUtcDate();
    }
}
