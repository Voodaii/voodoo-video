// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

global.testing = "this is a test";

console.log("main : loading universal site style...");
global.css = require("../css/site.css");

//!! We'll put some logic here that will allow users to choose their theme.
console.log("main : loading ui theme style...");
global.themeCss = require("../css/themes/slate-bootstrap.css");

// Bundle scripts that are used universally throughout the app.
console.log("main : loading universal site scripts...");

// Load jQuery.
console.log("main : loading jQuery...");
global.jQuery = require("jquery/dist/jquery");

// Load jQuery UI.
console.log("main : loading jQuery UI...");
global.jQueryUi = require("jquery-ui-dist/jquery-ui.min");
global.JQueryUiExternal = require("jquery-ui-dist/external/jquery/jquery");

