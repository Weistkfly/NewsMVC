import React, { createContext, useEffect, useState } from "react";
import axios from "axios";
import Search from "./Components/Search";
import CategoriesDropdown from "./Components/CategoriesDropdown";
import CountriesDropdown from "./Components/CountriesDropdown";

export const NewsContext = createContext();

export const NewsContextProvider = (props) => {
  const [data, setData] = useState();

  //Countries
  const handleCountries = async (countriesFilter) => {
    axios
    .get(
      `https://localhost:44305/api/news/${countriesFilter}}`
    )
    .then((response) => setData(response.data))
    .catch((error) => console.log(error));
      }

  //Categories
  const handleCategory = async (categoriesFilter) => {
    axios
    .get(
      `https://localhost:44305/api/news/${categoriesFilter}}`
    )
    .then((response) => setData(response.data))
    .catch((error) => console.log(error));
      }
   
  //Search    
  const handleSearch = async (searchTerm) => {
    axios
    .get(
      `https://localhost:44305/api/news/${searchTerm}`
    )
    .then((response) => setData(response.data))
    .catch((error) => console.log(error));
      }
   
  //Default
  useEffect(() => {
    axios
      .get(
        `https://localhost:44305/api/news`
      )
      .then((response) => setData(response.data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <NewsContext.Provider value={{ data }}>
      <Search onSearch ={handleSearch}/>
      <CategoriesDropdown onChange = {handleCategory}/>
      <CountriesDropdown onChange = {handleCountries}/>
      {props.children}
    </NewsContext.Provider>
  );
};
