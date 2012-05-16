/// <reference path="_references.js" />

module("String.Format Tests");

test("Single string argument", function () {
    var test = "My name is {0}".format("Eric");

    equals(test, "My name is Eric", test);
});

test("Multiple string arguments", function () {
    var test = "My name is {0} and I am {1}".format("Eric", "26");

    equals(test, "My name is Eric and I am 26", test);
});

test("Multiple string arguments repeated", function () {
    var test = "My name is {0} and I am {1}, thats {0} and I'm {1}".format("Eric", "26");

    equals(test, "My name is Eric and I am 26, thats Eric and I'm 26", test);
});

test("Multiple number arguments", function () {
    var test = "Integers are {0},{1},{2},{3}".format(1,2,3,4);

    equals(test, "Integers are 1,2,3,4", test);
});

test("Multiple boolean arguments", function () {
    var test = "True is {0} and False is {1}".format(true, false);

    equals(test, "True is true and False is false", test);
});

module("StringBuilder Tests");

test("Single line build", function () {
    var b = new StringBuilder();

    b.append("Hi there");

    equals(b.toString(), "Hi there", b.toString());
});

test("Multiple String Builders are separate", function () {
    var a = new StringBuilder();
    var b = new StringBuilder();

    a.append("Something");
    b.append("Hi there");

    equals(b.toString(), "Hi there", b.toString());
    equals(a.toString(), "Something", a.toString());
});