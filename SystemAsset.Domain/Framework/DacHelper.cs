using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.Data;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace SystemAsset.Domain.Framework
{
    /// <summary>
    /// DAC을 구현할 때 <c>SqlDbHelper</c>를 편리하게 사용하기 위한 방법을 제공합니다.
    /// </summary>
    /// <remarks>
    /// 20050708 전승범
    /// 20050721 전승범 메서드 추가: SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, int startIndex, int length, params SqlParameter[] sqlParams)
    /// 20091120 조현 Generic 메소드 추가 : SelectMultipleEntities, SelectSingleEntity
    /// </remarks>
    public class DacHelper : IDacHelper
    {
        private SqlDbHelper dbHelper;

        /// <summary>
        /// <c>SqlDbHelper</c>를 사용하여 <c>DacHelper</c>클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="dbHelper">Sql처리를 수행하는 <c>SqlDbHelper</c></param>
        public DacHelper(SqlDbHelper dbHelper)
        {
            if (dbHelper == null) throw new ArgumentNullException("dbHelper");
            this.dbHelper = dbHelper;
        }
        /// <summary>
        /// <c>SqlDbHelper</c>와 연결문자열 이름을 사용하여 <c>DacHelper</c>클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="dbHelper">Sql처리를 수행하는 <c>SqlDbHelper</c></param>
        /// <param name="connectionStringName">연결문자열 이름</param>
        public DacHelper(SqlDbHelper dbHelper, string connectionStringName)
            : this(dbHelper)
        {
            dbHelper.ConnectionStringName = connectionStringName;
        }

        #region IDacHelper 멤버
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫번째 엔티티 개체를 반환합니다. 나머지 엔티티 개체는 무시됩니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>첫번째 엔티티 개체</returns>
        public virtual object SelectSingleEntity(Type entityType, DataMappingCollection dataMapping, string query, params SqlParameter[] sqlParams)
        {
            return SelectSingleEntity(entityType, dataMapping, CommandType.Text, query, sqlParams);
        }
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫번째 엔티티 개체를 반환합니다. 나머지 엔티티 개체는 무시됩니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>첫번째 엔티티 개체</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public virtual object SelectSingleEntity(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams)
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            ArrayList rows = new ArrayList();

            dbHelper.FillToObjectList(query,
                      entityType, rows, sqlParams,
                      cmdType, dataMapping);

            //logging.PostProcess(rows.Count);

            return (rows.Count == 0) ? null : rows[0];
        }

        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫번째 엔티티 개체를 반환합니다. 나머지 엔티티 개체는 무시됩니다.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>첫번째 엔티티 개체</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public T SelectSingleEntity<T>(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams) where T : class
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            System.Collections.Generic.List<T> rows = new System.Collections.Generic.List<T>();

            dbHelper.FillToObjectList(query,
                entityType, rows, sqlParams,
                cmdType, dataMapping);

            //logging.PostProcess(rows.Count);

            return (rows.Count == 0) ? null : rows[0];
        }

        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 모든 엔티티 개체를 반환합니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        public virtual Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, string query, params SqlParameter[] sqlParams)
        {
            return SelectMultipleEntities(entityType, dataMapping, CommandType.Text, query, sqlParams);
        }
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 모든 엔티티 개체를 반환합니다.
        /// </summary>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public virtual Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams)
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            ArrayList rows = new ArrayList();

            dbHelper.FillToObjectList(query,
                entityType, rows, sqlParams,
                cmdType, dataMapping);

            //logging.PostProcess(rows.Count);

            return rows.ToArray(entityType);
        }


        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 모든 엔티티 개체를 반환합니다.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entityType">엔티티 형식</param>
        /// <param name="dataMapping"><c>DataMappingCollection</c></param>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>엔티티 개체 배열</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public virtual System.Collections.Generic.List<T> SelectMultipleEntities<T>(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, params SqlParameter[] sqlParams) where T : class
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            System.Collections.Generic.List<T> rows = new System.Collections.Generic.List<T>();

            dbHelper.FillToObjectList(query,
                entityType, rows, sqlParams,
                cmdType, dataMapping);
            //logging.PostProcess(rows.Count);

            return rows;
        }

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
        ///[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public virtual Array SelectMultipleEntities(Type entityType, DataMappingCollection dataMapping, CommandType cmdType, string query, int startIndex, int length, params SqlParameter[] sqlParams)
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            ArrayList rows = new ArrayList();
            dbHelper.FillToObjectList(query,
                entityType, rows, sqlParams,
                cmdType, startIndex, length, dataMapping);

            //logging.PostProcess(rows.Count);

            return rows.ToArray(entityType);
        }

        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열 또는 결과 집합이 비어있을 경우 null 참조</returns>
        public virtual object SelectScalar(string query, params SqlParameter[] sqlParams)
        {
            return SelectScalar(CommandType.Text, query, sqlParams);
        }
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="dbNullDefault">반환된 값 형식이 <c>DBNull</c>일 경우 반환할 값</param>
        /// <param name="nullDefault">반환된 값이 null 참조일 경우 반환할 값</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열</returns>
        public virtual object SelectScalar(string query, object dbNullDefault, object nullDefault, params SqlParameter[] sqlParams)
        {
            return SelectScalar(CommandType.Text, query, dbNullDefault, nullDefault, sqlParams);
        }
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열 또는 결과 집합이 비어있을 경우 null 참조</returns>
        public virtual object SelectScalar(CommandType cmdType, string query, params SqlParameter[] sqlParams)
        {
            return SelectScalar(cmdType, query, null, DBNull.Value, sqlParams);
        }
        /// <summary>
        /// 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 추가 열이나 행은 무시됩니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="dbNullDefault">반환된 값 형식이 <c>DBNull</c>일 경우 반환할 값</param>
        /// <param name="nullDefault">반환된 값이 null 참조일 경우 반환할 값</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>결과 집합의 첫 행의 첫 열</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Select)]
        public virtual object SelectScalar(CommandType cmdType, string query, object dbNullDefault, object nullDefault, params SqlParameter[] sqlParams)
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            object ret = dbHelper.ExecuteScalar(query, sqlParams, cmdType);

            if (ret == null) ret = nullDefault;
            else if (ret is DBNull) ret = dbNullDefault;

            //logging.PostProcess((null == ret || ret is DBNull) ? 0 : 1);

            return ret;
        }
        /// <summary>
        /// 연결에 대한 Transact-SQL 문을 실행하고 영향을 받는 행의 수를 반환합니다.
        /// </summary>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>영향 받는 행의 수</returns>
        public virtual int Execute(string query, params SqlParameter[] sqlParams)
        {
            return Execute(CommandType.Text, query, sqlParams);
        }
        /// <summary>
        /// 연결에 대한 Transact-SQL 문을 실행하고 영향을 받는 행의 수를 반환합니다.
        /// </summary>
        /// <param name="cmdType"><c>CommandType</c> 값 중 하나</param>
        /// <param name="query">실행할 Transact-SQL 문이나 저장 프로시저</param>
        /// <param name="sqlParams">Transact-SQL 문이나 지정 프로시저의 매개 변수</param>
        /// <returns>영향 받는 행의 수</returns>
        //[SupportSqlLogging(LogType = QueryLogType.Simple, QueryType = QueryType.Execute)]
        public virtual int Execute(CommandType cmdType, string query, params SqlParameter[] sqlParams)
        {
            //SqlLoggingProcessor logging = new SqlLoggingProcessor();

            //logging.PreProcess(this.dbHelper.ConnectionStringName, cmdType, query, sqlParams);

            int affected = dbHelper.ExecuteNonQuery(query, sqlParams, cmdType);

            //logging.PostProcess(affected);

            return affected;
        }
        /// <summary>
        /// 현재 데이터베이스 서버의 현지 날짜와 시간인 DateTime을 가져옵니다.
        /// </summary>
        /// <returns>현재 날짜와 시간 값을 갖는 <c>DateTime</c></returns>
        public virtual DateTime GetDate()
        {
            return (DateTime)dbHelper.ExecuteScalar("SELECT GETDATE()");
        }
        /// <summary>
        /// 이 컴퓨터의 현재 현지 날짜와 시간을 UTC로 표시한 DateTime을 가져옵니다.
        /// </summary>
        /// <returns>현재 UTC 날짜와 시간을 값으로 갖는 <c>DateTime</c></returns>
        public virtual DateTime GetUtcDate()
        {
            return (DateTime)dbHelper.ExecuteScalar("SELECT GETUTCDATE()");
        }
        #endregion
    }

}
