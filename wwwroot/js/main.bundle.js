!function(t,e){"object"==typeof exports&&"object"==typeof module?module.exports=e():"function"==typeof define&&define.amd?define([],e):"object"==typeof exports?exports.main=e():t.main=e()}(window,function(){return function(t){var e={};function n(r){if(e[r])return e[r].exports;var o=e[r]={i:r,l:!1,exports:{}};return t[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=t,n.c=e,n.d=function(t,e,r){n.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:r})},n.r=function(t){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},n.t=function(t,e){if(1&e&&(t=n(t)),8&e)return t;if(4&e&&"object"==typeof t&&t&&t.__esModule)return t;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var o in t)n.d(r,o,function(e){return t[e]}.bind(null,o));return r},n.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return n.d(e,"a",e),e},n.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},n.p="",n(n.s=1)}([,function(t,e,n){"use strict";function r(t,e,n,r,o){if(console.log("Draw: ",t,e,n,r,o),0==$(`#${t}`).children().length&&null!=e){const a=Math.max(Math.ceil(r/5),2),l=Array(Math.ceil(r/a)).fill(a).filter((t,e)=>e<4).map((t,e)=>t*(e+1));(new CalHeatMap).init(Object.assign({itemSelector:`#${t}`,data:e,start:new Date(n),cellSize:15,highlight:"now",legend:l,onClick:(e,n)=>DotNet.invokeMethodAsync("OMWReporting","CallFromJSAsync",t,e,n)},o))}}function o(t,e){if(0==$(`#${t}`).children().length&&null!=e){console.log("Chart: ",t,e);const n={top:20,right:30,bottom:40,left:200},r=800-n.left-n.right,o=200-n.top-n.bottom,a=d3v4.select(`#${t}`).attr("width",r+n.left+n.right).attr("height",o+n.top+n.bottom).append("g").attr("transform",`translate(${n.left},${n.top})`),l=d3v4.scaleLinear().domain([0,d3v4.max(e,t=>t.count)]).nice().range([0,r]);console.log("Scale x: ",l);const i=t=>`${t.status} - ${t.name} (${t.count})`,c=d3v4.scaleBand().domain(e.map(t=>i(t))).range([0,o]).padding(.1);a.append("g").attr("fill","steelblue").selectAll("rect").data(e).join("rect").attr("x",0).attr("y",t=>c(i(t))).attr("height",c.bandwidth()).attr("width",t=>l(t.count));const u=t=>t.attr("transform",`translate(0,${o})`).call(d3v4.axisBottom(l).tickSizeOuter(0)),f=t=>t.attr("tranform",`translate(${n.left},0)`).call(d3v4.axisLeft(c));a.append("g").call(u),a.append("g").call(f),a.append("text").attr("x",r/2).style("text-anchor","middle").text("Project Status")}}n.r(e),n.d(e,"CalHeatmap",function(){return r}),n.d(e,"ProjectsChart",function(){return o})}])});