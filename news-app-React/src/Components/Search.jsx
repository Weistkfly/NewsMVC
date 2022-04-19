import React from "react";

export default function Search(props){

    const handleFormShubmit = (event) => {
        event.preventDefault();
        const form = event.target;
        const searchInput = form.elements["search-input"];
        props.onSearch(searchInput.value);
    }

    return(
    <form onSubmit={handleFormShubmit}>
        <input type="text" placeholder="Search..." name="search-input"/>
        <button type="submit">Search</button>
    </form>
    );
}