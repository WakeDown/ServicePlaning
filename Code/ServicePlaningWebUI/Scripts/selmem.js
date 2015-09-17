//Required 
//sessionstorage.1.4.js 
//jQuery 2.1.0
//arrays.js

var selectedClass = 'warning';
var delimiter = ',';
var storageId = 'defaultStorage';
var tblId;
var cntPnlId;
var valPnlId;

function initSelectedMemmory(tableId, storeId, countPnlId, valuesPnlId) {
    tblId = tableId;
    cntPnlId = countPnlId;
    storageId = storeId;
    valPnlId = valuesPnlId;
    


    $('#' + tblId + ' tr')
          .filter(':has(:checkbox:checked)')
      .addClass(selectedClass)
      .end()
    /*.click(function (event) {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }
    })*/
      .find(':checkbox')
      .change(function (event) {
          $(this).parents('tr:first').removeClass(selectedClass).has(":checked").addClass(selectedClass);
          //alert($(this).prop("checked"));
          //if ($(this).prop("checked")) $(this).parents('tr:first').addClass(selectedClass); //.toggleClass(selectedClass);
          RememberOrRemoveValue($(this).val(), $(this).prop('checked'));
          if (cntPnlId) {
              var count = 0;
              var arr = GetStoredValues();
              $('#' + valPnlId).prop('value', arr);
              count = arr.length;
              $("#" + cntPnlId).text(count);
              
          }
      });

    //Check rows on load
   CheckDisplayRows();
}


function CheckDisplayRows() {
    $('#' + tblId + ' tr')
        .each(function () {
            var chk = $(this).find("td > :checkbox");
            var v = chk.val();
            //alert(v);
            if (typeof v !== "undefined") {
                if (v.length > 0) {
                    var ch = StorageHasValue(v);
                    chk.prop("checked", ch);
                    chk.trigger("change");
                }
            }
        });

    //function from common tristate 0.9.2 lib (my fnc)
    DefaultUpdate();
};

function RememberOrRemoveValue(val, remember) {
    var ids = '';
    var a = [];
    if (sessionStorage.length > 0 && sessionStorage.getItem(storageId) != null) {
        ids = sessionStorage.getItem(storageId);
        a = ids.split(delimiter); // массив
    }

    if (remember) {
        //Добавляем
        if (a.indexOf(val) < 0) {
            //не содержит такой же
            a.push(val);
        }
    } else {
        //Удаляем        
        a.remove(val);
    }

    ids = a.join(delimiter);
    if (ids != null && ids.length > 0 && ids.substring(0, 1) == delimiter) {
        ids = ids.substring(1, ids.length);
    }
    sessionStorage.setItem(storageId, ids);
}

function GetStoredValues() {
    var result = [];
    var arr = sessionStorage.getItem(storageId);
    if (arr != null && arr.length > 0) {
        result = arr.split(delimiter);
        result.sort(function (a, b) {
            return a - b;
        });
    }
    //result = aIds.join(delimiter);

    return result;
}

function ClearStorage() {
    sessionStorage.removeItem(storageId);
}

function StorageHasValue(val) {
    var result = false;
    if (typeof val !== "undefined") {
        if (val.length > 0) {
            var values = [];
            values = GetStoredValues();
            result = values.indexOf(val) >= 0;
        }
    }
    return result;
}

function ClearCheckedRows() {
    ClearStorage();
    CheckDisplayRows();
}

function RedirectWithValuesAndClear(url, paramName, otherParams) {
    url = typeof url !== "undefined" ? url : window.location.host;
    paramName = typeof paramName !== "undefined" ? paramName : "values";

    var values = new Array();
    values = GetStoredValues();
    
    if (typeof values !== "undefined" && values.length > 0) {
        ClearStorage();
        var newUrl = url + '?' + paramName + '=' + values; 
        if (otherParams.length > 0) newUrl = newUrl + '&' + otherParams;
        window.location = newUrl;
    } else {
        alert('Нет отмеченных значений!');
        return;
    }
}

var lstId;

//--selmem for no table
function initSelectedMemmoryNoTable(listId, storeId, countPnlId, valuesPnlId) {
    lstId = listId;
    cntPnlId = countPnlId;
    storageId = storeId;
    valPnlId = valuesPnlId;


    $('#' + lstId + ' :checkbox:not([id*=chkTristate])')
        //.filter(':has(:checkbox:checked)')
          //.addClass(selectedClass)
      //.end()
      /*.click(function (event) {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }
    })*/
    //.find(':checkbox')
        //.each(function (){
            //$(this)
                .change(function (event) {
                    //alert($(this).id);
          //$(this).parents('tr:first').removeClass(selectedClass).has(":checked").addClass(selectedClass);
          //alert($(this).prop("checked"));
          //if ($(this).prop("checked")) $(this).parents('tr:first').addClass(selectedClass); //.toggleClass(selectedClass);
          RememberOrRemoveValue($(this).val(), $(this).prop('checked'));
          if (cntPnlId) {
              var count = 0;
              var arr = GetStoredValues();
              $('#' + valPnlId).prop('value', arr);
              count = arr.length;
              $("#" + cntPnlId).text(count);

          }
      })
        //}
        //)
;

    //Check rows on load
    CheckDisplayRowsNoTable();
}

function CheckDisplayRowsNoTable() {
    $('#' + lstId + ' :checkbox:not([id*=chkTristate])')
        .each(function () {
            var chk = $(this);//.find("td > :checkbox");
            var v = chk.val();
            //alert(v);
            if (typeof v !== "undefined") {
                if (v.length > 0) {
                    var ch = StorageHasValue(v);
                    chk.prop("checked", ch);
                    chk.trigger("change");
                }
            }
        });

    //function from common tristate 0.9.2 lib (my fnc)
    //DefaultUpdate();//Не обновляем так как на странице План происходит вызов этой функции в коде
};

function ClearCheckedRowsNoTable() {
    ClearStorage();
    CheckDisplayRowsNoTable();
}