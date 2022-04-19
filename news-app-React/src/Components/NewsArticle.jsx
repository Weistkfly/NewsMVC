import React from "react";

function NewsArticle({ data }) {
  return (
    <div className="news">
      <h1 className="news__title">{data.title}</h1>
      <span className="news__author">Author:{data.author}</span> <br/>
      <p className="news__desc">{data.content}</p>
      <span className="news__published">{data.date}</span>
    </div>
  );
}

export default NewsArticle;