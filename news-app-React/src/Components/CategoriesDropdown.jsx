import React from "react";

export default function CategoriesDropdown(props) {
  
  const handleCategory = (e) => {
      props.onChange(e.target.value);
  };

  return (
    <div>
      <select onChange={handleCategory}>
        <option value="business">Business</option>
        <option value="entertainment">Entertainment</option>
        <option value="general">General</option>
        <option value="health">Health</option>
        <option value="science">Science</option>
        <option value="sports">Sports</option>
        <option value="technology">Technology</option>
      </select>
    </div>
  );
}

