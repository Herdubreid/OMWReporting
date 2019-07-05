/// <reference path="../node_modules/@types/cal-heatmap/index.d.ts" />
/// <reference path="../node_modules/@types/jquery/index.d.ts" />

declare class DotNet {
    static invokeMethodAsync(...args);
}

export function CalHeatmap(id: string, points: any, from: any, max: number, init: any) {
    console.log('Draw: ', id, points, from, max, init);
    if ($(`#${id}`).children().length == 0 && points != undefined) {
        const step = Math.max(Math.ceil(max / 5), 2);
        const legend = Array(Math.ceil(max / step))
            .fill(step)
            .filter((_, i) => i < 4)
            .map((step, i) => step * (i + 1));
        const map = new CalHeatMap();
        map.init({
            itemSelector: `#${id}`,
            data: points,
            start: new Date(from),
            cellSize: 15,
            highlight: 'now',
            legend,
            onClick: (date, nb) => DotNet.invokeMethodAsync('OMWReporting', 'CallFromJSAsync', id, date, nb),
            ...init
        });
    }
}
