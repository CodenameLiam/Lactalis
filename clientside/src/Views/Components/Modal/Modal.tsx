import * as React from "react";
import ReactModal, { Props as ModalProps } from "react-modal";
import * as classNames from "classnames";
import { store } from "Models/Store";

export interface IModalProps {
	/** Is the modal open */
	isOpen: boolean;
	/** A label for the modal for screen readers */
	label: string;
	/** The classname for the modal */
	className?: string;
	/** Function that will be run after the modal has opened. */
	onAfterOpen?: () => void;
	/** Function that will be run after the modal has closed. */
	onAfterClose?: () => void;
	/**
	 * Function that will be run when the modal is requested to be closed, prior to actually closing.
	 * If the isOpen prop is not changed in this callback then it will not actually close!
	 */
	onRequestClose?: (event: React.MouseEvent | React.KeyboardEvent) => void;
	/**
	 * Raw props to be passed to react-modal.
	 * This will overwrite any props that are placed on the modal by this component
	 */
	modalProps?: ModalProps;
}

const rootId = "root";
const modalElement = document.getElementById(rootId);

/**
 * A modal dialog that can display any content inside of it
 */
export default class Modal extends React.Component<IModalProps> {
	public render() {
		if (!modalElement) {
			throw new Error(`Could not find the #${rootId} element in the html. Could not create modal`);
		}

		return (
			<ReactModal
				className={classNames("modal-content", this.props.className)}
				overlayClassName={"modal-container"}
				portalClassName={classNames("portal", store.appLocation)}
				appElement={modalElement}
				isOpen={this.props.isOpen}
				onAfterOpen={this.props.onAfterOpen}
				onRequestClose={this.props.onRequestClose}
				contentLabel={this.props.label}
				shouldCloseOnEsc={true}
				shouldCloseOnOverlayClick={true}
				aria={{
					// @ts-ignore
					live: "assertive",
				}}
				{...this.props.modalProps}>
				{this.props.children}
			</ReactModal>
		);
	}
}
