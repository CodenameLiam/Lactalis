import { observer } from "mobx-react";
import * as React from "react";
import { action, observable } from "mobx";
import Modal, { IModalProps } from "./Modal";

type globalModalState = IModalProps & { children: React.ReactNode };

export interface IGlobalModal {
	/**
	 * Shows a modal on the screen
	 * @param label A label for the modal for screen readers
	 * @param children The content to display inside the modal
	 * @param modalProps
	 */
	show: (label: string, children: React.ReactNode, modalProps?: Partial<IModalProps>) => void;
	/**
	 * Hides the global dialog
	 */
	hide: () => void;
}

/**
 * A global modal that can be called imperatively from the store
 * This component should only be constructed by the top level App component
 */
@observer
export default class GlobalModal extends React.Component implements IGlobalModal {
	/** Defaults for the modal state */
	private get defaultModalState(): globalModalState {
		return {
			label: "",
			children: null,
			isOpen: false,
			className: undefined,
			onRequestClose: this.hide,
		};
	}

	/**
	 * The props of the modal controlled by this component
	 */
	@observable
	private modalState: globalModalState = this.defaultModalState;

	/** @inheritDoc */
	@action
	public show = (
		label: string,
		children: React.ReactNode,
		modalProps: Partial<IModalProps> = {}
	) => {
		this.modalState = {
			...this.defaultModalState,
			isOpen: true,
			children,
			label,
			...modalProps,
		};
	};

	/** @inheritDoc */
	@action
	public hide = () => {
		this.modalState.isOpen = false;
	};

	public render() {
		return <Modal {...this.modalState}>{this.modalState.children}</Modal>;
	}
}
