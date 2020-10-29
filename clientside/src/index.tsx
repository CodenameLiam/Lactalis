import "graphiql/graphiql.css";
import "react-toastify/dist/ReactToastify.css";
import "react-contexify/dist/ReactContexify.min.css";
import "semantic-ui-css/semantic.min.css";
import "./scss/main.scss";
import * as React from "react";
import * as ReactDOM from "react-dom";
import App from "./App";
import { store } from "./Models/Store";
import * as Models from "Models/Entities";

// Add the store to the global scope for easy debugging from the console
window["store"] = store;
window["Models"] = Models;

ReactDOM.render(<App />, document.getElementById("root") as HTMLElement);
