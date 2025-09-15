function findPathsAlgorithm(start, end, graph) {
    const pareto = {};
    const queue = [[1.0, 0, start, [start]]];
    const allowCycleToEnd = start === end;

    while (queue.length > 0) {
        const [prob, length, node, path] = queue.shift();
        if (!graph[node]) continue;

        for (const [neighbor, edgeProb] of graph[node]) {
            if (path.includes(neighbor) && !(allowCycleToEnd && neighbor === end)) continue;

            const newProb = prob * edgeProb;
            const newLength = length + 1;
            const newPath = [...path, neighbor];

            if (!pareto[neighbor]) pareto[neighbor] = [];

            let skip = false;
            const newList = [];

            for (const [pProb, pLen, pPath] of pareto[neighbor]) {
                if (pProb > newProb && pLen <= newLength) {
                    skip = true;
                    break;
                }
                if (!(newProb > pProb && newLength <= pLen)) {
                    newList.push([pProb, pLen, pPath]);
                }
            }

            if (skip) continue;

            newList.push([newProb, newLength, newPath]);
            pareto[neighbor] = newList;
            if (!(allowCycleToEnd && neighbor === end)) {
                queue.push([newProb, newLength, neighbor, newPath]);
            }
        }
    }

    const results = pareto[end] || [];
    if (results.length === 0) return [];

    results.sort((a, b) => a[1] - b[1]);

    const finalResults = [];
    const seenProb = new Set();

    for (const [prob, length, path] of results) {
        if (!seenProb.has(prob)) {
            finalResults.push([prob, length, path]);
            seenProb.add(prob);
        }
    }

    return finalResults;
}
