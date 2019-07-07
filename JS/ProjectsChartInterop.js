// /// <reference path="../node_modules/@types/d3/index.d.ts" />
export function ProjectsChart(id, projects) {
    if ($(`#${id}`).children().length == 0 && projects != undefined) {
        console.log('Chart: ', id, projects);
        // Graph Parameters
        const margin = { top: 20, right: 30, bottom: 40, left: 200 };
        const width = 800 - margin.left - margin.right;
        const height = 200 - margin.top - margin.bottom;
        // Graph Canvas
        const svg = d3v4.select(`#${id}`)
            .attr('width', width + margin.left + margin.right)
            .attr('height', height + margin.top + margin.bottom)
            .append('g')
            .attr('transform', `translate(${margin.left},${margin.top})`);
        // Scales
        const x = d3v4.scaleLinear()
            .domain([0, d3v4.max(projects, d => d.count)]).nice()
            .range([0, width]);
        console.log('Scale x: ', x);
        const yLabel = d => `${d.status} - ${d.name} (${d.count})`;
        const y = d3v4.scaleBand()
            .domain(projects.map(d => yLabel(d)))
            .range([0, height])
            .padding(.1);
        // Bars
        svg.append('g')
            .attr('fill', 'steelblue')
            .selectAll('rect')
            .data(projects)
            .join('rect')
            .attr('x', 0)
            .attr('y', d => y(yLabel(d)))
            .attr('height', y.bandwidth())
            .attr('width', d => x(d.count));
        // Axis
        const xAxis = g => g
            .attr('transform', `translate(0,${height})`)
            .call(d3v4.axisBottom(x).tickSizeOuter(0));
        const yAxis = g => g
            .attr('tranform', `translate(${margin.left},0)`)
            .call(d3v4.axisLeft(y));
        svg.append('g')
            .call(xAxis);
        svg.append('g')
            .call(yAxis);
        // Label
        svg.append('text')
            .attr('x', width / 2)
            .style('text-anchor', 'middle')
            .text('Project Status');
    }
}
//# sourceMappingURL=ProjectsChartInterop.js.map