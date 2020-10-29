import * as React from "react";
import { observer } from "mobx-react";
import { Checkbox } from "../Checkbox/Checkbox";
import If from "../If/If";
import { ICollectionItemActionProps, actionFilterFn } from "./Collection";
import { observable, runInAction } from "mobx";
import { IOrderByCondition } from "../ModelCollection/ModelQuery";
import { DisplayType } from "../Models/Enums";

type nameFn = (name: string) => string | React.ReactNode;
type transformFn<T> = (item: T, name: string) => string | React.ReactNode;

export interface ICollectionHeaderProps<T> {
	name: string;
	displayName: string | nameFn;
	sortable?: boolean;
	transformItem?: transformFn<T>;
	nullValue?: string;
	sortClicked?: (
		event: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>
	) => IOrderByCondition<T> | undefined | void;
}

export interface ICollectionHeaderPropsPrivate<T> extends ICollectionHeaderProps<T> {
	headerName?: string | React.ReactNode;
}

export interface ICollectionHeadersProps<T> {
	headers: Array<ICollectionHeaderPropsPrivate<T>>;
	actions?: Array<ICollectionItemActionProps<T>> | actionFilterFn<T>;
	selectableItems?: boolean;
	allChecked: boolean;
	onCheckedAll?: (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => void;
	/** The default order by condition */
	orderBy?: IOrderByCondition<T> | undefined;
}

@observer
export default class CollectionHeaders<T> extends React.Component<ICollectionHeadersProps<T>> {
	@observable
	private orderBy: IOrderByCondition<T> | undefined | void;

	constructor(props: ICollectionHeadersProps<T>, context: any) {
		super(props, context);
		const { orderBy } = this.props;
		this.orderBy = orderBy;
	}

	public render() {
		const { selectableItems, headers, actions } = this.props;

		return (
			<thead>
				<tr className="list__header">
					<If condition={selectableItems}>
						<th className="select-box">{this.renderSelectAllCheckbox()}</th>
					</If>
					{headers.map((header, idx) => {
						return (
							<th
								key={idx}
								scope="col"
								onClick={(event) => {
									runInAction(() => {
										if (header.sortClicked) {
											this.orderBy = header.sortClicked(event);
										}
									});
								}}
								className={
									header.sortable
										? !this.orderBy || this.orderBy.path !== header.name
											? "sortable"
											: this.orderBy.descending
											? "sortable--des"
											: "sortable--asc"
										: ""
								}>
								{header.headerName ? header.headerName : `Column ${idx}`}
							</th>
						);
					})}
					<If condition={actions != null}>
						<th scope="col" className="list__header--actions"></th>
					</If>
				</tr>
			</thead>
		);
	}

	public renderSelectAllCheckbox() {
		const { allChecked, onCheckedAll } = this.props;
		const checkboxDisplayType = DisplayType.INLINE;

		return (
			<Checkbox
				label="Select All"
				modelProperty="checked"
				name="selectall"
				model={{}}
				displayType={checkboxDisplayType}
				inputProps={{
					checked: allChecked,
					onChange: (event) => {
						runInAction(() => {
							if (onCheckedAll) {
								onCheckedAll(event, event.target.checked);
							}
						});
					},
				}}
			/>
		);
	}
}
