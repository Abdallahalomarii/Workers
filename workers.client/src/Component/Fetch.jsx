import { useEffect } from "react";
import { useState } from "react";
import axios from 'axios';
import Cookies from 'js-cookie';

function Fetch() {
    const [fetchData, setfetchData] = useState([]);

    async function getWorkListing() {
        //const token = data.data.token;
        const token = Cookies.get('token');
        const config = {
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        };
        try {
            const response = await axios.get('https://localhost:7230/api/WorkListings', config);
            setfetchData(response.data);
        }
        catch (error) {
            console.error(error)
        }
    }
    useEffect(() => {
        if (fetchData.count !== 0)
            getWorkListing();
    }, []);
    return (
        <>
        <div className="test">
                {fetchData.map(item => {
                    return( <li key={item.id}>
                        {item.name}
                    </li>
                    )
            })
            }
            </div>
        </>
    )
}
export default Fetch;