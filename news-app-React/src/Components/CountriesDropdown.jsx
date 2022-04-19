import React from "react";

export default function CountriesDropdown(props) {
  
  const handleCountry = (e) => {
      props.onChange(e.target.value);
  };

  return (
    <div>
      <select onChange={handleCountry}>
        <option value="ar">Argentina</option>
        <option value="au">Australia</option>
        <option value="kr">South Korea</option>
        <option value="ca">Canada</option>
        <option value="cn">China</option>
        <option value="jp">Japan</option>
        <option value="ru">Rusisa</option>
        <option value="ua">Ukraine</option>
        <option value="us">Unites States</option>
        <option value="br">Brazil</option>
        <option value="mx">Mexico</option>
        <option value="de">Germany</option>
      </select>
    </div>
  );
}