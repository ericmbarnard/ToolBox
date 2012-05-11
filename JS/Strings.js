/*
=======================================================================
    Description: String Libary
    Created On: 2012-05-11
    Author: Eric M. Barnard

							Change Log
    2012-05-11 - emb - created
=======================================================================
*/

;(function () {

    "use-strict";

    /*
    * String.format
    */
    String.prototype.format = function () {
        var args = [].slice.call(arguments || []);

        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof( args[number] ) != 'undefined'
              ? args[number]
              : match
            ;
        });
    };

    /*
    * String Builder
    */
    function StringBuilder() {
        var self = this;
        var lines = [];

        function buildLine(line) {
            var args = [].slice.call(arguments),
                replacers = [],
                formatted = '';

            if (args.length > 1) {
                replacers = args.slice(1, args.length - 1);
            }

            line = (typeof (line) == 'string' ? line : line.toString());
            formatted = line.format(replacers);

            return formatted;
        };

        this.append = function (line) {
            var formatted = buildLine(arguments);
            lines.push(formatted);
        };

        this.insert = function (index, line) {
            var args = [].slice.call(arguments),
                passedArgs = args.slice(1, args.length - 1),
                formatted = '';

            formatted = buildLine(passedArgs);

            lines.splice(index, 0, formatted);
        };

        this.toString = function () {
            var args = [].slice.call(arguments || []),
                separator = args[0] || '\r\n';
                        
            return lines.join(separator);
        };
    };

    window.StringBuilder = StringBuilder;

}());