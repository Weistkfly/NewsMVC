import React, { createContext, useEffect, useState } from "react";
import axios from "axios";
import { NewsContextProvider } from "./NewsContext";
import News from "./Components/News";
import "./App.css";

function App() {
  return (
    <NewsContextProvider>
      <News />
    </NewsContextProvider>
  );
}

export default App;