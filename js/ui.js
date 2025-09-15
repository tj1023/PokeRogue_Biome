let selectedField = null;

function formatPathWithProbability(path, graph) {
    let formattedPath = [];
    
    for (let i = 0; i < path.length; i++) {
        const current = path[i];
        
        // 이전 노드에서 현재 노드로의 확률 찾기
        let probability = 1.0;
        if (i > 0) {
            const previous = path[i - 1];
            const edges = graph[previous];
            
            if (edges) {
                for (const [neighbor, prob] of edges) {
                    if (neighbor === current) {
                        probability = prob;
                        break;
                    }
                }
            }
        }
        
        // 지역 이름에 확률에 따른 색상과 표기 적용
        if (probability === 0.5) {
            formattedPath.push(`<span class="probability-50">${current}(50%)</span>`);
        } else if (Math.abs(probability - 1/3) < 0.001) {
            formattedPath.push(`<span class="probability-33">${current}(33%)</span>`);
        } else {
            formattedPath.push(current);
        }
        
        // 화살표 추가 (마지막이 아닌 경우)
        if (i < path.length - 1) {
            formattedPath.push(`<span class="arrow">→</span>`);
        }
    }
    
    return formattedPath.join('');
}

function createLocationGrid() {
    const locations = Object.keys(graph).sort();
    const grid = document.getElementById('locationGrid');
    
    grid.innerHTML = locations.map(location => 
        `<div class="location-item" data-location="${location}">${location}</div>`
    ).join('');

    grid.querySelectorAll('.location-item').forEach(item => {
        item.addEventListener('click', () => selectLocation(item.dataset.location));
    });
}

function selectLocation(location) {
    if (selectedField === null) {
        document.getElementById('startInput').value = location;
        selectedField = 'end';
    } else {
        document.getElementById('endInput').value = location;
        selectedField = null;
    }
}

function findPaths() {
    const start = document.getElementById('startInput').value.trim();
    const end = document.getElementById('endInput').value.trim();
    const resultsDiv = document.getElementById('results');

    if (!start || !end) {
        alert('시작점과 도착점을 모두 입력해주세요.');
        return;
    }

    if (!graph[start]) {
        alert(`"${start}"는 존재하지 않는 지역입니다.`);
        return;
    }

    const results = findPathsAlgorithm(start, end, graph);

    if (results.length === 0) {
        resultsDiv.innerHTML = `
            <h3>검색 결과</h3>
            <div class="no-result">${start}에서 ${end}로 가는 경로가 없습니다.</div>
        `;
    } else {
        let html = `<h3>🔍 검색 결과: ${start} → ${end}</h3>`;
        
        results.forEach(([prob, length, path], index) => {
            const pathStr = formatPathWithProbability(path, graph);
            const percentage = Math.round(prob * 100);
            
            html += `
                <div class="path-item">
                    <div class="path-number">${index + 1}. 길이 ${length}단계</div>
                    <div class="path-route">${pathStr}</div>
                    <div class="path-details">성공 확률: ${percentage}%</div>
                </div>
            `;
        });

        resultsDiv.innerHTML = html;
    }

    resultsDiv.style.display = 'block';
}
