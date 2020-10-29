import * as React from "react";
import { store } from "../../../Models/Store";
import { Button, Display } from "../Button/Button";

export interface IConfirmModalOptions {
	/** The text to be placed in the confirm button */
	confirmText?: string;
	/** The text to be placed in the cancel button */
	cancelText?: string;
}

/**
 * Opens a confirm modal which gives a user an option to confirm or cancel
 * @param title The title on the modal
 * @param message The message to display in the modal
 * @param options Extra options for the modal
 * @returns A promise that is resolved on the user confirming or rejected on the user cancelling
 */
export function confirmModal(
	title: string,
	message: React.ReactNode,
	options: IConfirmModalOptions = {}
) {
	return new Promise((resolve, reject) => {
		const onConfirm = () => {
			store.modal.hide();
			resolve();
		};

		const onCancel = () => {
			store.modal.hide();
			reject();
		};

		const confirmText = options.confirmText || "Confirm";
		const cancelText = options.cancelText || "Cancel";

		const confirmDom = (
			<>
				<div key="header" className="modal__header">
					<h2 key="header-item" className="modal__title">
						{title}
					</h2>
					<Button
						key="header-cancel"
						className="modal--close"
						icon={{ icon: "square-x", iconPos: "icon-left" }}
						display={Display.Text}
						onClick={onCancel}
						labelVisible={false}>
						{cancelText}
					</Button>
				</div>
				<div key="message" className="modal__message">
					{message}
				</div>
				<div key="actions" className="modal__actions">
					<Button
						className="modal--confirm"
						key={"confirm"}
						onClick={onConfirm}
						display={Display.Solid}>
						{confirmText}
					</Button>
					<Button
						className="modal--cancel"
						key={"cancel"}
						onClick={onCancel}
						display={Display.Outline}>
						{cancelText}
					</Button>
				</div>
			</>
		);

		store.modal.show(title, confirmDom, {
			className: "confirm-modal",
			onRequestClose: () => {
				store.modal.hide();
				reject();
			},
		});
	});
}

export interface IAlertModalProps {
	/** The text to be placed in the cancel button */
	cancelText?: string;
}

/**
 * Displays an alert on the screen
 * @param title A title for the modal
 * @param message The message content for the modal
 * @param options Extra options for the modal
 * @returns A promise that is resolved when the modal is closed
 */
export function alertModal(
	title: string,
	message: React.ReactNode,
	options: IAlertModalProps = {}
) {
	return new Promise((resolve) => {
		const onClose = () => {
			store.modal.hide();
			resolve();
		};

		const cancelText = options.cancelText || "Cancel";

		const alertDom = (
			<>
				<div key="header" className="modal__header">
					<h2 key="header-item" className="modal__title">
						{title}
					</h2>
					<Button
						key="header-cancel"
						icon={{ icon: "square-x", iconPos: "icon-left" }}
						display={Display.Text}
						onClick={onClose}
						labelVisible={false}>
						{cancelText}
					</Button>
				</div>
				<div key="message" className="modal__message">
					{message}
				</div>
			</>
		);

		store.modal.show(title, alertDom, {
			className: "alert-modal",
			onRequestClose: () => {
				store.modal.hide();
				resolve();
			},
		});
	});
}
