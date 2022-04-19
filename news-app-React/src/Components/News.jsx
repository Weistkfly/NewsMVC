import React, { useContext } from "react";
import { NewsContext } from "../NewsContext";
import NewsArticle from "./NewsArticle";
import Search from "./Search";

function News(props) {
  const { data } = useContext(NewsContext);
  console.log(data);

  return (
    <div>
      <h1 className="head__text">News App</h1>
      <div className="all__news">
        {data
          ? data.map((news) => (
              <NewsArticle data={news} key={news.id} />
            ))
          : "Loading"}
      </div>
    </div>
  );
}

export default News;