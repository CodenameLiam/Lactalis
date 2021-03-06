import { observer } from "mobx-react";
import * as React from "react";
import { ICollectionItemActionProps, expandFn, actionFilterFn } from "./Collection";
import { observable, computed, runInAction } from "mobx";
import { Checkbox } from "../Checkbox/Checkbox";
import { Button } from "../Button/Button";
import { ButtonGroup, Alignment } from "../Button/ButtonGroup";
import { ICollectionHeaderPropsPrivate } from "./CollectionHeaders";
import classNames from "classnames";
import * as moment from "moment";
import If from "../If/If";
import {
	EntityContextMenu,
	IEntityContextMenuActions,
} from "../EntityContextMenu/EntityContextMenu";

export interface ICollectionRowProps<T> {
	item: T;
	headers: Array<ICollectionHeaderPropsPrivate<T>>;
	actions?: Array<ICollectionItemActionProps<T>> | actionFilterFn<T>;
	actionsMore?: IEntityContextMenuActions<T>;
	selectableItems?: boolean;
	expandAction?: expandFn<T>;
	checked?: boolean;
	onChecked?: (
		event: React.ChangeEvent<HTMLInputElement>,
		checked: boolean,
		checkedItem: T
	) => void;
	idColumn?: string;
	keyValue: string;
	dataFields?: (row: T) => { [key: string]: string };
}

/**
 * This is a row in a collection component
 */
@observer
class CollectionRow<T> extends React.Component<ICollectionRowProps<T>, any> {
	@observable
	private expanded = false;

	@observable
	private checked = { checked: this.props.checked };

	private moreMenuRef: EntityContextMenu<T> | null;

	/**
	 * The dom for the expanded row
	 */
	@computed
	private get expandDom() {
		if (this.props.expandAction) {
			// The magic number is since we have an extra column for the checkbox and another for the actions
			const colSpanOffset = this.props.selectableItems ? 2 : 1;
			return (
				<tr
					className={classNames(
						"collection__item",
						"collection__item--is-expanded-child",
						this.expanded ? "" : " hide"
					)}>
					<td colSpan={this.props.headers.length + colSpanOffset}>
						{this.props.expandAction(this.props.item)}
					</td>
				</tr>
			);
		} else {
			return null;
		}
	}

	constructor(props: ICollectionRowProps<T>, context: any) {
		super(props, context);
		this.checked.checked = this.props.checked;
	}

	public render() {
		const columns = [];

		// The checkbox at the start of the row
		if (!!this.props.selectableItems) {
			columns.push(
				<td key="0" className="select-box">
					<Checkbox
						model={{}}
						modelProperty=""
						name="select"
						inputProps={{
							checked: this.props.checked,
							onChange: (event) => {
								if (this.props.onChecked) {
									this.props.onChecked(event, event.target.checked, this.props.item);
								}
							},
						}}
					/>
				</td>
			);
		}

		// The columns from the item to display
		columns.push(
			this.props.headers.map((column, itemIdx) => {
				let displayValue: any;

				if (column.transformItem) {
					displayValue = column.transformItem(this.props.item, column.name);
				} else if (this.props.item[column.name] || this.props.item[column.name] === 0) {
					if (typeof this.props.item[column.name]["toLocaleDateString"] === "function") {
						displayValue = moment(this.props.item[column.name]).format("DD/MM/YYYY");
					} else if (typeof this.props.item[column.name]["toString"] === "function") {
						displayValue = this.props.item[column.name]["toString"]();
					} else {
						displayValue = this.props.item;
					}
				} else {
					displayValue = column.nullValue || "None";
				}

				return <td key={itemIdx + 1}>{displayValue}</td>;
			})
		);

		// The action buttons
		let actionButtons: JSX.Element[] = [];

		if (typeof this.props.actions === "function") {
			actionButtons = this.props.actions(this.props.item).map((action, actIdx) => {
				if (!action.customButton) {
					const icon =
						action.showIcon && action.icon && action.iconPos
							? { icon: action.icon, iconPos: action.iconPos }
							: undefined;
					return (
						<Button
							key={actIdx}
							className={action.buttonClass}
							icon={icon}
							buttonProps={{
								onClick: (event) => {
									action.action(this.props.item, event);
								},
							}}>
							{action.label}
						</Button>
					);
				}

				return <React.Fragment key={actIdx}>{action.customButton(this.props.item)}</React.Fragment>;
			});
		} else if (Array.isArray(this.props.actions)) {
			actionButtons = this.props.actions.map((action, actIdx) => {
				if (!action.customButton) {
					const icon =
						action.showIcon && action.icon && action.iconPos
							? { icon: action.icon, iconPos: action.iconPos }
							: undefined;
					return (
						<Button
							key={actIdx}
							className={action.buttonClass}
							icon={icon}
							buttonProps={{
								onClick: (event) => {
									action.action(this.props.item, event);
								},
							}}>
							{action.label}
						</Button>
					);
				}

				return <React.Fragment key={actIdx}>{action.customButton(this.props.item)}</React.Fragment>;
			});
		}

		// The expand button if needed
		let expandButton = null;
		if (this.props.expandAction) {
			expandButton = (
				<Button
					icon={
						this.expanded
							? { icon: "chevron-up", iconPos: "icon-top" }
							: { icon: "chevron-down", iconPos: "icon-top" }
					}
					buttonProps={{
						onClick: () => {
							runInAction(() => {
								this.expanded = !this.expanded;
							});
						},
					}}>
					Expand
				</Button>
			);
		}

		if (
			expandButton ||
			this.props.actions ||
			(this.props.actionsMore && this.props.actionsMore.length > 0)
		) {
			columns.push(
				<td className="list__items--actions" key={this.props.headers.length + 1}>
					<ButtonGroup alignment={Alignment.HORIZONTAL}>
						{actionButtons}
						{expandButton}
						<If condition={!!this.props.actionsMore && !!this.props.actionsMore.length}>
							<EntityContextMenu
								menuId={this.props.keyValue}
								actions={this.props.actionsMore || []}
								ref={(ref) => {
									this.moreMenuRef = ref || null;
								}}
								entity={this.props.item}
							/>
							<Button
								onClick={(event: React.MouseEvent<Element, MouseEvent>) => {
									if (this.moreMenuRef) {
										this.moreMenuRef.handleContextMenu(event);
									}
								}}
								icon={{ icon: "more-horizontal", iconPos: "icon-top" }}>
								More
							</Button>
						</If>
					</ButtonGroup>
				</td>
			);
		}

		let prefixedDataFields = {};
		if (this.props.dataFields) {
			const dataFields = this.props.dataFields(this.props.item);
			Object.keys(dataFields).forEach((key) => {
				prefixedDataFields[`data-${key}`] = dataFields[key];
			});
		}

		return (
			<>
				<tr
					className={classNames(
						"collection__item",
						this.expanded ? "collection__item--has-expanded-child" : null,
						this.checked.checked ? "collection__item--selected" : null
					)}
					data-id={this.props.idColumn ? this.props.item[this.props.idColumn] : undefined}
					{...prefixedDataFields}>
					{columns}
				</tr>
				{this.expandDom}
			</>
		);
	}
}

export default CollectionRow;
