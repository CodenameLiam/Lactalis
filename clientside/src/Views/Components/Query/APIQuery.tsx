import * as React from "react";
import { observer } from "mobx-react";
import { observable, action } from "mobx";
import axios, { AxiosResponse } from "axios";

export interface QueryResult {
	data: any;
	error?: string;
	success?: boolean;
	loading: boolean;
}

export interface IAPIQueryProps {
	children: (result: QueryResult) => React.ReactNode;
	url: string;
}

@observer
class APIQuery extends React.Component<IAPIQueryProps> {
	@observable
	private requestState: "loading" | "error" | "done" = "loading";

	private requestData: any;

	private requestError?: string;

	public componentDidMount = () => {
		const url = this.props.url;

		axios.get(url).then(this.onSuccess).catch(this.onError);
	};

	@action
	private onSuccess = (data: AxiosResponse) => {
		this.requestData = data.data;
		this.requestState = "done";
	};

	@action
	private onError = (data: AxiosResponse) => {
		this.requestData = data.data;
		this.requestState = "error";
		this.requestError = data.statusText;
	};

	public render() {
		return this.props.children({
			loading: this.requestState === "loading",
			success: this.requestState === "done",
			error: this.requestError,
			data: this.requestData,
		});
	}
}

export default APIQuery;
