import { observable, computed, action } from "mobx";
import { observer } from "mobx-react";

export interface IPaginationQueryOptions {
	page: number;
	perPage: number;
}

export class PaginationQueryOptions implements IPaginationQueryOptions {
	@observable
	public page: number = 0;
	@observable
	public perPage = 10;

	@action
	public gotoPage = (pageNo: number) => {
		this.page = pageNo;
	};
}

export default interface IPaginationData {
	queryOptions: PaginationQueryOptions;
	totalRecords: number;
}
