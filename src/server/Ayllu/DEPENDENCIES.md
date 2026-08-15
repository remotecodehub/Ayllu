# Arquivo de Documentação do Projeto (.NET Dependency Architecture)

Este documento Markdown contém o grafo interativo e flexível do projeto `.csproj` alvo e suas dependências.

<link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/mudblazor@9.5.1/_content/MudBlazor/MudBlazor.min.css" rel="stylesheet" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.29.2/cytoscape.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/webcola@3.4.0/WebCola/cola.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/cytoscape-cola@2.5.1/cytoscape-cola.min.js"></script>

<style>
    .dependency-graph-wrapper {
        background-color: #1a1a2e;
        color: #f5f5f5;
        font-family: 'Roboto', sans-serif;
        padding: 16px;
        border-radius: 8px;
        margin: 20px 0;
        box-shadow: 0px 4px 12px rgba(0,0,0,0.4);
    }
    .dependency-header {
        background-color: #232338;
        border: 1px solid #33334c;
        padding: 16px;
        margin-bottom: 16px;
        border-radius: 6px;
        display: flex;
        align-items: center;
        justify-content: space-between;
    }
    .dependency-title-container {
        display: flex;
        align-items: center;
    }
    .dependency-title {
        color: #fff;
        margin: 0;
        font-size: 1.25rem;
        font-weight: bold;
    }
    .dependency-subtitle {
        color: #b0b0c6;
        font-size: 0.85rem;
    }
    .graph-container-box {
        height: 500px;
        background-color: #111122;
        border-radius: 8px;
        border: 1px solid #2b2b40;
        position: relative;
        box-shadow: 0px 4px 12px rgba(0,0,0,0.5);
        overflow: hidden;
    }
    .cy-canvas {
        width: 100%;
        height: 100%;
        position: absolute;
        top: 0;
        left: 0;
        z-index: 1;
    }
    .loading-overlay-box {
        position: absolute;
        top: 0; left: 0; width: 100%; height: 100%;
        background: rgba(17, 17, 34, 0.9);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 10;
        border-radius: 8px;
        flex-direction: column;
    }
    .btn-reorganize {
        background-color: transparent;
        color: #8e8eb2;
        border: 1px solid #33334c;
        padding: 8px 16px;
        border-radius: 4px;
        font-weight: 500;
        cursor: pointer;
        transition: all 0.2s ease-in-out;
    }
    .btn-reorganize:hover {
        background-color: #252545;
        color: #ffffff;
        border-color: #594ae2;
    }
    .btn-sync-action {
        background-color: #594ae2;
        color: #ffffff;
        border: none;
        padding: 8px 16px;
        border-radius: 4px;
        font-weight: 500;
        cursor: pointer;
        margin-left: 8px;
    }
    .btn-sync-action:hover {
        background-color: #6c5ce7;
    }
    .spinner-box {
        border: 4px solid rgba(255,255,255,0.1);
        width: 42px;
        height: 42px;
        border-radius: 50%;
        border-left-color: #594ae2;
        animation: spin-anim 1s linear infinite;
        margin-bottom: 12px;
    }
    .setup-zone-box {
        border: 2px dashed #594ae2;
        padding: 30px;
        border-radius: 8px;
        text-align: center;
        background: #1b1b32;
        cursor: pointer;
        transition: background 0.3s, border-color 0.3s;
        max-width: 480px;
    }
    .setup-zone-box:hover {
        background: #252545;
        border-color: #00e676;
    }
    @keyframes spin-anim { 0% { transform: rotate(0deg); } 100% { transform: rotate(360deg); } }
</style>

<div class="dependency-graph-wrapper">
    <div class="dependency-header">
        <div class="dependency-title-container">
            <svg style="width:32px;height:32px;margin-right:12px;fill:#594ae2" viewBox="0 0 24 24">
                <path d="M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,4A2,2 0 0,1 14,6A2,2 0 0,1 12,8A2,2 0 0,1 10,6A2,2 0 0,1 12,4M12,18A2,2 0 0,1 10,16A2,2 0 0,1 12,14A2,2 0 0,1 14,16A2,2 0 0,1 12,18M17,14A2,2 0 0,1 15,12A2,2 0 0,1 17,10A2,2 0 0,1 19,12A2,2 0 0,1 17,14M7,14A2,2 0 0,1 5,12A2,2 0 0,1 7,10A2,2 0 0,1 9,12A2,2 0 0,1 7,14Z"/>
            </svg>
            <div>
                <h5 class="dependency-title">.NET Target Project Dependency Architecture</h5>
                <span class="dependency-subtitle" id="target-csprojs-info">Sincronizando workspace local...</span>
            </div>
        </div>
        <div>
            <button class="btn-reorganize" onclick="resetTargetLayout()">Reorganizar Layout</button>
            <button id="btn-sync-target" class="btn-sync-action" style="display: none;" onclick="initTargetWorkspacePermission()">Vincular Pasta Local</button>
        </div>
    </div>
    <div class="graph-container-box">
        <div id="cy-target" class="cy-canvas"></div>
           <div id="loading-target" class="loading-overlay-box">
            <div class="spinner-box"></div>
            <p id="loading-text-target" style="color: #b0b0c6; font-size: 14px;">Verificando estrutura do diretório...</p>
        </div>
        <div id="welcome-message-target" class="loading-overlay-box" style="display: none;">
            <div class="setup-zone-box" onclick="initTargetWorkspacePermission()">
                <svg style="width:48px;height:48px;color:#594ae2;margin-bottom:12px;" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 11V7a4 4 0 118 0m-4 10v2m0-6V4M5 21h14a2 2 0 002-2v-5a2 2 0 00-2-2H5a2 2 0 00-2 2v5a2 2 0 002 2z"></path>
                </svg>
                <p style="color: #fff; font-size: 18px; font-weight: bold; margin-bottom: 6px;">Vincular diretório do projeto</p>
                <p style="color: #b0b0c6; font-size: 13px;">
                    Clique para autorizar a leitura do arquivo <b>.csproj</b> alvo e de seus projetos de dependência localizados na solução/diretório.
                </p>
            </div>
        </div>
    </div>
</div>

<script type="module">
    
    import { get, set } from 'https://cdn.jsdelivr.net/npm/idb-keyval@6/+esm';

    let cyInstance = null;
    const projectFilesMap = new Map();
    const DB_KEY = 'dotnet_target_dir_handle';
    let targetProjectName = "";

    window.addEventListener('DOMContentLoaded', async () => {
        try {
            const retainedHandle = await get(DB_KEY);
            if (retainedHandle) {
                if (await verifyPermission(retainedHandle, true)) {
                    await scanAndBuildTargetGraph(retainedHandle);
                    return;
                }
            }
            showSetupUI();
        } catch (err) {
            console.error(err);
            showSetupUI();
        }
    });

    async function verifyPermission(fileHandle, readWrite) {
        const options = {};
        if (readWrite) options.mode = 'read';
        if ((await fileHandle.queryPermission(options)) === 'granted') return true;
        if ((await fileHandle.requestPermission(options)) === 'granted') return true;
        return false;
    }

    async function initTargetWorkspacePermission() {
        try {
            const dirHandle = await window.showDirectoryPicker();
            await set(DB_KEY, dirHandle);
            document.getElementById('welcome-message-target').style.display = 'none';
            document.getElementById('loading-target').style.display = 'flex';
            await scanAndBuildTargetGraph(dirHandle);
        } catch (err) {
            if (err.name !== 'AbortError') {
                alert('Permissão necessária para ler e mapear os arquivos locais do projeto.');
            }
            showSetupUI();
        }
    }

    function showSetupUI() {
        document.getElementById('loading-target').style.display = 'none';
        document.getElementById('welcome-message-target').style.display = 'flex';
        document.getElementById('btn-sync-target').style.display = 'inline-block';
    }

    async function scanAndBuildTargetGraph(dirHandle) {
        document.getElementById('loading-text-target').innerText = "Identificando .csproj alvo e varrendo arquivos...";
        projectFilesMap.clear();

        await deepScanProjects(dirHandle);

        let targetName = null;
        for (const [name, data] of projectFilesMap.entries()) {
            if (data.isSameLevel) {
                targetName = name;
                break;
            }
        }

        if (!targetName && projectFilesMap.size > 0) {
            targetName = projectFilesMap.keys().next().value;
        }

        if (!targetName) {
            alert('Nenhum projeto .csproj foi encontrado no diretório selecionado.');
            showSetupUI();
            return;
        }

        targetProjectName = targetName;
        document.getElementById('target-csprojs-info').innerText = `Projeto Alvo: ${targetName}.csproj`;

        buildTargetSubGraph(targetName);
    }

    async function deepScanProjects(dirHandle, isRoot = true) {
        for await (const entry of dirHandle.values()) {
            if (entry.kind === 'file' && entry.name.endsWith('.csproj')) {
                const file = await entry.getFile();
                const text = await file.text();
                const projName = entry.name.replace('.csproj', '');
                projectFilesMap.set(projName, {
                    content: text,
                    isSameLevel: isRoot
                });
            } else if (entry.kind === 'directory') {
                if (!['bin', 'obj', '.git', '.vs', 'node_modules'].includes(entry.name)) {
                    await deepScanProjects(entry, false);
                }
            }
        }
    }

    function buildTargetSubGraph(targetName) {
        const elements = [];
        const visitedNodes = new Set();
        const edgesToProcess = [];

        function traverseDependencies(currentName) {
            if (visitedNodes.has(currentName)) return;
            visitedNodes.add(currentName);

            const projInfo = projectFilesMap.get(currentName);
            if (!projInfo) return;

            const content = projInfo.content;
            const isExecutable = content.includes('Sdk="Microsoft.NET.Sdk.Web"') || content.includes('Sdk="Microsoft.NET.Sdk.BlazorWebAssembly"');
            const isTarget = (currentName === targetName);

            elements.push({
                data: {
                    id: currentName,
                    label: (isTarget ? '🎯 ' : (isExecutable ? '🟢 ' : '📦 ')) + currentName,
                    type: isTarget ? 'target' : (isExecutable ? 'web' : 'lib')
                }
            });

            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(content, "text/xml");
            const projectReferences = xmlDoc.getElementsByTagName("ProjectReference");

            for (let i = 0; i < projectReferences.length; i++) {
                const includePath = projectReferences[i].getAttribute("Include");
                if (includePath) {
                    const refName = includePath.replace(/\\/g, '/').split('/').pop().replace('.csproj', '');
                    edgesToProcess.push({ source: currentName, target: refName });
                    traverseDependencies(refName);
                }
            }
        }

        traverseDependencies(targetName);

        for (const edge of edgesToProcess) {
            if (visitedNodes.has(edge.source) && visitedNodes.has(edge.target)) {
                elements.push({
                    data: {
                        id: `edge-${edge.source}-${edge.target}`,
                        source: edge.source,
                        target: edge.target
                    }
                });
            }
        }

        renderInteractiveTargetGraph(elements);
    }

    function renderInteractiveTargetGraph(elements) {
        cyInstance = cytoscape({
            container: document.getElementById('cy-target'),
            elements: elements,
            wheelSensitivity: 0.05,
            style: [
                {
                    selector: 'node',
                    style: {
                        'label': 'data(label)',
                        'shape': 'round-rectangle',
                        'background-color': '#2b2b40',
                        'border-color': '#594ae2',
                        'border-width': 2,
                        'text-valign': 'center',
                        'text-halign': 'center',
                        'color': '#ffffff',
                        'font-family': 'Roboto, sans-serif',
                        'font-size': '13px',
                        'width': 'label',
                        'padding': '14px'
                    }
                },
                {
                    selector: 'node[type="target"]',
                    style: {
                        'background-color': '#4a148c',
                        'border-color': '#ab47bc',
                        'border-width': 3,
                        'font-weight': 'bold'
                    }
                },
                {
                    selector: 'node[type="web"]',
                    style: {
                        'background-color': '#1b5e20',
                        'border-color': '#00e676'
                    }
                },
                {
                    selector: 'edge',
                    style: {
                        'width': 2.5,
                        'line-color': '#594ae2',
                        'target-arrow-color': '#00e676',
                        'target-arrow-shape': 'triangle',
                        'curve-style': 'bezier',
                        'control-point-step-size': 40
                    }
                }
            ],
            layout: {
                name: 'cola',
                animate: true,
                maxSimulationTime: 2000,
                fit: true,
                padding: 50,
                nodeSpacing: 65,
                edgeLength: 150
            }
        });

        cyInstance.ready(() => {
            document.getElementById('loading-target').style.display = 'none';
        });
    }

    function resetTargetLayout() {
        if (cyInstance) {
            cyInstance.layout({
                name: 'cola',
                animate: true,
                maxSimulationTime: 1500,
                fit: true,
                padding: 50,
                nodeSpacing: 65
            }).run();
        }
    }

    window.initTargetWorkspacePermission = initTargetWorkspacePermission;
    window.resetTargetLayout = resetTargetLayout;
</script>
