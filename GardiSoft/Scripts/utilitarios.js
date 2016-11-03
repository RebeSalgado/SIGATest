(function ($) {
    $.fn.extend({


        calendario: function (options) {

            defaults = {
                format: "yyyy-mm-dd",
                todayBtn: true,
                language: "es",
                orientation: "bottom auto",
                autoclose: true,
                todayHighlight: true
            };

            var options = $.extend({}, defaults, options);


            $(this).datepicker(options);
        },



        trabajadores: function (options) {

            defaults = {

            };

            var $objeto = $(this);
         
            var options = $.extend({}, defaults, options);

            $($objeto).typeahead({
                source: function (query, process) {
                    var nombres = [];

                    var obj = {};
                    obj.busqueda = $($objeto).val();                
                    obj.cantidad = 20;
                    // console.log($(caja).val());
                    $.ajax({
                        url: 'http://informatica.gardilcic.cl/webservice/rrhhService.svc/BuscarTrabajadores',
                        type: "POST",
                        async: false,
                        data: JSON.stringify(obj),
                        dataType: "JSON",
                        success: function (datos) {
                            var objeto = eval(datos.d);

                            $.map(objeto, function (item) {
                                //console.log(item);
                                var nombre = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.Nombre;
                                var itm = {
                                    rut: item.Rut,
                                    name: nombre,
                                    id: item.Id,
                                    cargo: item.Cargo,                                 
                                    toString: function () {
                                        return JSON.stringify(this);
                                    },
                                    toLowerCase: function () {
                                        return this.name.toLowerCase();
                                    },
                                    indexOf: function (string) {
                                        return String.prototype.indexOf.apply(this.name, arguments);
                                    },
                                    replace: function (string) {
                                        var value = '';
                                        value += this.name;
                                        if (typeof (this.level) != 'undefined') {
                                            value += ' <span class="pull-right muted">';
                                            value += this.level;
                                            value += '</span>';
                                        }
                                        return String.prototype.replace.apply('<div style="padding: 1px 2px; font-size: 12px;">' + value + '</div>', arguments);
                                    }
                                };
                                nombres.push(itm);
                            });
                            return process(nombres);
                        }
                    });
                },
                matcher: function () {
                    return true;
                },
                property: 'name',
                items: 10,
                minLength: 3,
                updater: function (item) {
                    var item = JSON.parse(item);                 
                    $(options.Nombre).val(item.name);
                    $(options.Cargo).val(item.cargo);
                    return item.rut;
                }
            });

        },


        tabla: function (options) {

            defaults = {
            
            };

            var options = $.extend({}, defaults, options);

        
       
         return    $(this).DataTable({
                dom: 'Bfrtip',
                buttons: [
                     'excel'
                ],
               // "bSort": false
            });

         

        },


        jsonTabla: function (options){
    

            defaults = {
                tabla: "",
                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todo"]]
            };

            var options = $.extend({}, defaults, options);

            var $objeto = $(this);

            if (options.tabla.length == 0) {
                $objeto.find('table').remove();
            };

            $objeto.addClass('table-responsive');

            var names = Object.keys(options.tabla[0]);
            var texto = "";

            texto += "<table class='table table-hover'>";
            texto += "<thead><tr>";
            for (var i = 0; i < names.length; i++) {
                texto += "<td>" + names[i] + "</td>";
            }


            texto += "</thead></tr>";

            texto += "<tbody>";

            for (var i = 0; i < options.tabla.length; i++) {
                texto += "<tr>";
                for (var j = 0; j < names.length; j++) {
                    texto += "<td>" + options.tabla[i][names[j]] + "</td>";
                }
                texto += "</tr>";
            }
            
            texto += "</tbody>";

            texto += "</table>";
        
            $objeto.html(texto);

            

            $objeto.find('table').DataTable({
                dom: 'Blfrtip',
                buttons: [
                     'excel',
                    
                ],
                "order": []
                ,lengthMenu: options.lengthMenu
            });
        
        },

        fileUpload: function (options) {


            defaults = {

                 url : ""
            };

            var options = $.extend({}, defaults, options);

            $(this).on('change', function (e) {
                var files = e.target.files;
                //var myID = 3; //uncomment this to make sure the ajax URL works
                if (files.length > 0) {
                    if (window.FormData !== undefined) {
                        var data = new FormData();
                        for (var x = 0; x < files.length; x++) {
                            data.append("file" + x, files[x]);
                        }

                        $.ajax({
                            type: "POST",
                            url: options.url,
                            contentType: false,
                            processData: false,
                            data: { data : data, id:  3},
                            success: function (result) {
                               
                            },
                            error: function (xhr, status, p3, p4) {
                                var err = "Error " + " " + status + " " + p3 + " " + p4;
                                if (xhr.responseText && xhr.responseText[0] == "{")
                                    err = JSON.parse(xhr.responseText).Message;
                               
                            }
                        });
                    } else {
                        alert("This browser doesn't support HTML5 file uploads!");
                    }
                }
            });


        }



    }) 

    })(jQuery);

    
    $.extend({
        notificar: function (opciones)
        {
            defaults ={
                title: '',
                text: '',
                type: 'success'

            };
            var parametros = $.extend({}, defaults, opciones);
             

            new PNotify({
                title: parametros.title,
                text: parametros.text,
                type: parametros.type
            });
        }
    });

    String.prototype.toDate = function () {
        var fecha = new Date(parseInt(this.substr(6)));

        var yyyy = fecha.getFullYear().toString();
        var mm = (fecha.getMonth() + 1).toString(); // getMonth() is zero-based         
        var dd = fecha.getDate().toString();

        return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
    };