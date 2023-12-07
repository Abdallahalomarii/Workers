import { useEffect, useState } from 'react';
import axios from 'axios';

function App() {
    const [workers, setWorkers] = useState();

    useEffect(() => {
        populateWorkersData();
    }, []);

    const contents = workers === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>name</th>
                    <th>location</th>
                    <th>phone</th>
                    <th>pricePerHour</th>
                </tr>
            </thead>
            <tbody>
                {workers.map(worker =>
                    <tr key={worker.workerID}>
                        <td>{worker.name}</td>
                        <td>{worker.location}</td>
                        <td>{worker.phone}</td>
                        <td>{worker.pricePerHour}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWorkersData() {
        const apiUrl = 'https://localhost:7230/api/IndustrialWorkers';
        //fetch(apiUrl, {
        //    method: 'GET',
        //    headers: {
        //        'Accept': 'application/json',
        //    },
        //})
        //    .then(response => response.json())
        //    .then(data => setWorkers(data))
        //    .catch(error => console.error('Error:', error));

        axios.get(apiUrl)
            .then(res => {
                setWorkers(res.data);
            })
            .catch(error => {
                console.log('Error', error);
            });

    }
}

export default App;