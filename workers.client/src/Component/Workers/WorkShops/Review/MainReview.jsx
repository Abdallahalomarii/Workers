import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import Cookies from 'js-cookie';

import 'bootstrap/dist/css/bootstrap.min.css';

function MainReview() {
    const { workshopId } = useParams();
    const [reviews, setReviews] = useState({});
    const [newReview, setNewReview] = useState({
        Comment: '',
        Rating: 0,
    });

    const FetchReviews = async () => {
        const token = Cookies.get('token');
        const config = {
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        };
        try {
            const response = await axios.get(`https://localhost:7230/api/Review/${workshopId}`, config);
            if (response.status === 200) {
                setReviews(response.data);
            }
        } catch (error) {
            console.log(error);
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewReview({
            ...newReview,
            [name]: value,
        });
    };

    const submitReview = async () => {
        const token = Cookies.get('token');
        const config = {
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        };
        try {
            const response = await axios.post(
                `https://localhost:7230/api/Review/${workshopId}`,
                newReview,
                config
            );
            if (response.status === 200) {
                // Review submitted successfully, update the reviews list
                FetchReviews();
                // Reset the newReview state
                setNewReview({
                    Comment: '',
                    Rating: 0,
                });
            }
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        FetchReviews();
    }, []);

    return (
        <div className="container mt-5">
            <h1 className="mb-4">Workshop Reviews</h1>

            <div className="mb-4">
                <h2>Add a Review</h2>
                <div className="mb-3">
                    <label htmlFor="comment" className="form-label">
                        Comment:
                    </label>
                    <input
                        type="text"
                        id="comment"
                        name="Comment"
                        value={newReview.Comment}
                        onChange={handleInputChange}
                        className="form-control"
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="rating" className="form-label">
                        Rating:
                    </label>
                    <input
                        type="number"
                        id="rating"
                        name="Rating"
                        value={newReview.Rating}
                        onChange={handleInputChange}
                        className="form-control"
                    />
                </div>
                <button onClick={submitReview} className="btn btn-primary">
                    Submit Review
                </button>
            </div>

            <div>
                <h2>Recent Reviews</h2>
                {reviews.map((review) => (
                    <div key={review.WorkshopID} className="mb-3">
                        <p className="mb-1">Comment: {review.comment}</p>
                        <p className="mb-0">Rating: {review.rating}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default MainReview;
