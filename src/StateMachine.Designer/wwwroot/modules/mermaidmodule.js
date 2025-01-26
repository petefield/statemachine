import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@9/dist/mermaid.esm.min.mjs';

export function Initialize() {
    mermaid.initialize({ startOnLoad: true, flowchart: { useMaxWidth: true } });
}

export function Render(componentId, definition) {
    
    var elements = document.getElementsByClassName(componentId);

    for(const element of elements)
    {
        const diagramdefinition = htmlDecode(element);
        const id = "mmd" + Math.round(Math.random() * 10000);

        mermaid.render(`${id}-mermaid-svg`, diagramdefinition, (svg, bind) => {
            element.innerHTML = svg
        });
    }
}

function htmlDecode(input) {
     var doc = new DOMParser().parseFromString(input.dataset.diagram, "text/html");
    return   doc.documentElement.textContent;
}