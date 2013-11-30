/*!
* jQuery.parseJSON() extension (supports ISO & Asp.net date conversion)
*
* Version 1.0 (13 Jan 2011)
*
* Copyright (c) 2011 Robert Koritnik
* Licensed under the terms of the MIT license
* http://www.opensource.org/licenses/mit-license.php
*/
(function ($) {

  // JSON RegExp
  var rvalidchars = /^[\],:{}\s]*$/;
  var rvalidescape = /\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g;
  var rvalidtokens = /"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g;
  var rvalidbraces = /(?:^|:|,)(?:\s*\[)+/g;
  var dateISO = /\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:[.,]\d+)?Z/i;
  var dateNet = /\/Date\((\d+)(?:-\d+)?\)\//i;

  // replacer RegExp
  var replaceISO = /"(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})(?:[.,](\d+))?Z"/i;
  var replaceNet = /"\\\/Date\((\d+)(?:-\d+)?\)\\\/"/i;

  // determine JSON native support
  var nativeJSON = (window.JSON && window.JSON.parse) ? true : false;
  var extendedJSON = nativeJSON && window.JSON.parse('{"x":9}', function (k, v) { return "Y"; }) === "Y";

  var jsonDateConverter = function (key, value) {
    if (typeof (value) === "string") {
      if (dateISO.test(value)) {
        return new Date(value);
      }
      if (dateNet.test(value)) {
        return new Date(parseInt(dateNet.exec(value)[1], 10));
      }
    }
    return value;
  };

  $.extend({
    parseJSON: function (data, convertDates) {
      /// <summary>Takes a well-formed JSON string and returns the resulting JavaScript object.</summary>
      /// <param name="data" type="String">The JSON string to parse.</param>
      /// <param name="convertDates" optional="true" type="Boolean">Set to true when you want ISO/Asp.net dates to be auto-converted to dates.</param>

      if (typeof data !== "string" || !data) {
        return null;
      }

      // Make sure leading/trailing whitespace is removed (IE can't handle it)
      data = $.trim(data);

      // Make sure the incoming data is actual JSON
      // Logic borrowed from http://json.org/json2.js
      if (rvalidchars.test(data
                .replace(rvalidescape, "@")
                .replace(rvalidtokens, "]")
                .replace(rvalidbraces, ""))) {
        // Try to use the native JSON parser
        if (extendedJSON || (nativeJSON && convertDates !== true)) {
          return window.JSON.parse(data, convertDates === true ? jsonDateConverter : undefined);
        }
        else {
          data = convertDates === true ?
                        data.replace(replaceISO, "new Date(parseInt('$1',10),parseInt('$2',10)-1,parseInt('$3',10),parseInt('$4',10),parseInt('$5',10),parseInt('$6',10),(function(s){return parseInt(s,10)||0;})('$7'))")
                            .replace(replaceNet, "new Date($1)") :
                        data;
          return (new Function("return " + data))();
        }
      } else {
        $.error("Invalid JSON: " + data);
      }
    }
  });
})(jQuery);


/*
*  JsonPostDataConverter
*  Recursive Json Data Converter
*  2012-02-07 김명훈
*/
(function ($) {
  $.JsonPostDataConverter = function (data) {

    ConvertData = function (node) {

      /* Child Node Iteration */
      for (var prop in node) {
        currentProp = node[prop];

        /* Date 형의 경우엔 ISO string형으로 변환 */
        if (typeof currentProp === 'number') { continue; }
        if (typeof currentProp === 'string') { continue; }
        if (typeof currentProp === 'object' && currentProp.getDate !== undefined) /* Date 형일 경우 */
        { node[prop] = currentProp.toString(); continue; }

        if (typeof currentProp === 'object' && prop !== 'undefined' && prop !== '__proto__') ConvertData(currentProp);
      }

      return;
    }

    ConvertData(data);

    return data;
  }
})(jQuery);