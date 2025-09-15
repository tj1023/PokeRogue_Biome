let selectedField = null;

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
            const pathStr = path.join(' → ');
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
