// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var gridFilters = [];
var sort = "";
var pageSize;
const urlParams = new URLSearchParams(window.location.search);

function initSearchQuery() {
    // Load state from query string
    var v = getQueryFromUrl('PageSize');
    if (v) pageSize = parseInt(v, 10);
    v = getQueryFromUrl('Sort');
    if (v) sort = v;
    v = getQueryFromUrl('Filters');
    if (v) gridFilters = JSON.parse(v);
}
initSearchQuery();

function initSearchComponents() {
    var elements = document.querySelectorAll("[data-search]");

    for (var i = 0; i < elements.length; i++) {
        var e = elements[i];
        var searchProp = e.dataset.search;
        var searchValue = null;
        for (var fi in gridFilters) {
            if (gridFilters[fi].Property === searchProp)
                searchValue = gridFilters[fi].Value.toString();
        }
        if (e.tagName === 'SELECT') {
            if (searchValue) for (var oi in e.options) if (e.options[oi].value == searchValue) e.selectedIndex = oi;
            e.addEventListener('change', function () { updateSearchQuery(this.dataset.search, getDropDownValue(this)); });
        }
        else if (e.tagName === 'INPUT') {
            if (searchValue) e.value = searchValue;
            e.addEventListener('change', function () { updateSearchQuery(this.dataset.search, this.value); });
        }
    }
}
initSearchComponents();

function getDropDownValue(dropdown) {
    if (dropdown.selectedIndex < 0) return null;
    var value = dropdown.options[dropdown.selectedIndex].value;
    if (value === "") return null;
    if (/^[0-9]{1,}/.test(value))
        return parseInt(value, 10);
    return value;
}

function updateSearchQuery(propName, value) {


    switch (propName) {
        case "Page":
            window.location = window.location.pathname + replaceUrlQuery(window.location.search, "Page", value);
            break;
        case "PageSize":
            window.location = window.location.pathname + replaceUrlQuery(window.location.search, "PageSize", value);
            break;
        case "Sort":
            window.location = window.location.pathname + replaceUrlQuery(window.location.search, "Sort", value);
            break;
        default:
            var idx = -1;
            for (var i in gridFilters) if (gridFilters[i].Property == propName) { idx = i; }
            if (idx >= 0) gridFilters.splice(idx, 1);
            if (value || value === 0)
            gridFilters.push({
                "Property": propName,
                "Value": value
            });

            window.location = window.location.pathname + replaceUrlQuery(window.location.search, "Filters", gridFilters.length ? JSON.stringify(gridFilters) : null);
            break;
    }
}

function getQueryFromUrl(key) {
    return urlParams.get(key);
}

function replaceUrlQuery(url, key, value) {
    var re = new RegExp("[\\?&]" + key + "=([^&#]*)", "i"), match = re.exec(url), delimiter, newString;

    if (match === null) {
        if (value === null) return url;
        var hasQuestionMark = /\?/.test(url);
        delimiter = hasQuestionMark ? "&" : "?";
        newString = url + delimiter + key + "=" + value;
    } else {
        delimiter = match[0].charAt(0);
        if (value === null)
            newString = url.replace(re, '');
        else
            newString = url.replace(re, delimiter + key + "=" + value);

        if (newString.indexOf('?') < 0)
            newString = newString.replace('&', '?');
    }

    return newString;
}

function convertToWeek(s) {
    var i = parseInt(s, 10);
    var year = Math.floor(i / 1000);
    var week = i % 1000;
    return 'wk' + week + ',' + year;
}