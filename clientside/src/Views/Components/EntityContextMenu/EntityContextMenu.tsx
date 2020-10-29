import * as React from "react";
import { observer } from "mobx-react";
import { contextMenu } from "react-contexify";
import {
	IContextMenuProps,
	ContextMenu,
	IContextMenuItemProps,
	isItemGroup,
	IContextMenuItemGroup,
} from "../ContextMenu/ContextMenu";
import { MenuItemEventHandler } from "react-contexify/lib/types";

export interface IEntityContextMenuItemActionProps<T> extends IContextMenuItemProps {
	/** Callback function on click with entity */
	onEntityClick: (args: MenuItemEventHandler, entity: T) => any;
	condition?: (model: T) => boolean;
}

interface IEntityContextMenuItemGroup<T> extends IContextMenuItemGroup {
	actions: IEntityContextMenuActions<T>;
}

export type IEntityContextMenuActions<T> = Array<
	IEntityContextMenuItemActionProps<T> | IEntityContextMenuItemGroup<T>
>;

interface IEntityContextMenuProps<T> extends IContextMenuProps {
	actions: IEntityContextMenuActions<T>;
	entity: T;
}

@observer
export class EntityContextMenu<T> extends React.Component<IEntityContextMenuProps<T>> {
	public render() {
		return (
			<ContextMenu
				{...this.props}
				actions={this.props.actions
					.filter((action) =>
						isItemGroup(action) ? true : action.condition?.(this.props.entity) ?? true
					)
					.map(this.tranformAction)}
			/>
		);
	}

	private tranformAction = (
		action: IEntityContextMenuItemActionProps<T> | IEntityContextMenuItemGroup<T>
	): IEntityContextMenuItemActionProps<T> | IEntityContextMenuItemGroup<T> => {
		if (!isItemGroup(action)) {
			return this.addEntityProps(action);
		} else {
			return {
				...action,
				actions: action.actions.map(this.tranformAction),
			};
		}
	};

	private addEntityProps = (action: IEntityContextMenuItemActionProps<T>) => {
		const newOnClick =
			action.onClick ||
			((args: MenuItemEventHandler) => {
				action.onEntityClick(args, this.props.entity);
			});

		return {
			...action,
			onClick: newOnClick,
		};
	};

	public handleContextMenu = (e: React.MouseEvent<Element, MouseEvent>) => {
		// always prevent default behavior
		e.preventDefault();

		contextMenu.show({
			id: this.props.menuId,
			event: e,
		});
	};
}
