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
    * StringBuilder
    */
    function StringBuilder() {
        // wrapper for an array
    };
    
    // we will make the prototype of the StringBuilder an array and then just
    // tack on extra functions to the prototype
    var PROTO = [];

    // private function for building formatted strings
    PROTO._buildLine = function () {
        var args = this.slice.call(arguments),
            replacers = [],
            len = args.length,
            line = args[0],
            formatted = '';

        if (len > 1) {
            replacers = args.slice(1, args.length - 1);
        }

        // if we werent' passed a string, convert it into one
        line = (typeof (line) === "string") ? line : line.toString();

        // if we have multiple args, then we need to format the line
        formatted = (len > 1) ? line : line.format(replacers);

        return formatted;
    };

    PROTO.append = function (line) {
        var formatted = this._buildLine.apply(this, arguments);
        this.push(formatted);
        return this;
    };

    // appends a string into the builder and adds a CRLF
    // also allows for an empty call to just add a CRLF
    PROTO.appendLine = function (line) {
        this.append.apply(this, arguments);
        this.push("\r\n");
        return this;
    };

    // inserts a line at a certain index in the StringBuilder
    PROTO.insert = function (index, line) {
        var args = this.slice.call(arguments),
            passedArgs = args.slice(1, args.length - 1),
            formatted = '';

        formatted = this._buildLine.apply(this, passedArgs);

        this.splice(index, 0, formatted);

        return this;
    };

    PROTO.toString = function () {
        var args = this.slice.call(arguments || []),
            separator = args[0] || '';
                        
        return this.join(separator);
    };

    // assign the prototype to our special array
    StringBuilder.prototype = PROTO;

    // make sure it is available globally
    window.StringBuilder = StringBuilder;

}());