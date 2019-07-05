/// <reference path="../node_modules/@types/cal-heatmap/index.d.ts" />
/// <reference path="../node_modules/@types/jquery/index.d.ts" />
export function CalHeatmap(id, points, from, max, init) {
    console.log('Draw: ', id, points, from, max, init);
    if ($(`#${id}`).children().length == 0 && points != undefined) {
        const step = Math.max(Math.ceil(max / 5), 2);
        const legend = Array(Math.ceil(max / step))
            .fill(step)
            .filter((_, i) => i < 4)
            .map((step, i) => step * (i + 1));
        const map = new CalHeatMap();
        map.init(Object.assign({ itemSelector: `#${id}`, data: points, start: new Date(from), cellSize: 15, highlight: 'now', legend, onClick: (date, nb) => DotNet.invokeMethodAsync('OMWReporting', 'CallFromJSAsync', id, date, nb) }, init));
    }
}
//# sourceMappingURL=CalHeatmapInterop.js.map