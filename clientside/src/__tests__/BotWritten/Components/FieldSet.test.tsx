import React from "react";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { Form } from "Views/Components/Form/Form";
import { Button } from "Views/Components/Button/Button";
import { TextField } from "Views/Components/TextBox/TextBox";

let container: HTMLElement | null;

beforeEach(() => {
	container = document.createElement("div");
	document.body.appendChild(container);
});

afterEach(() => {
	if (container) {
		document.body.removeChild(container);
		container = null;
	}
});

describe("Form component", () => {
	it("Display Submit Button", () => {
		act(() => {
			ReactDOM.render(<Form submitButton={true} />, container);
		});

		if (!!container) {
			const submitButton = container.querySelector(".submit");
			expect(submitButton).toBeInstanceOf(HTMLButtonElement);
		}
	});

	it("Hide Submit Button", () => {
		act(() => {
			ReactDOM.render(<Form />, container);
		});

		if (!!container) {
			const submitButton = container.querySelector(".submit");
			expect(submitButton).toBeNull();
		}
	});

	it("Display Cancel Button", () => {
		act(() => {
			ReactDOM.render(<Form cancelButton={true} />, container);
		});

		if (!!container) {
			const cancelButton = container.querySelector(".cancel");
			expect(cancelButton).toBeInstanceOf(HTMLButtonElement);
		}
	});

	it("Hide Cancel Button", () => {
		act(() => {
			ReactDOM.render(<Form />, container);
		});

		if (!!container) {
			const cancelButton = container.querySelector(".cancel");
			expect(cancelButton).toBeNull();
		}
	});

	it("Display passed in Action Groups instead of the default actions", () => {
		act(() => {
			const actionGroups = [<Button className="custom-action">Custom Action</Button>];
			ReactDOM.render(
				<Form submitButton={true} cancelButton={true} actionGroups={actionGroups} />,
				container
			);
		});

		if (!!container) {
			const customButton = container.querySelector(".custom-action");
			expect(customButton).toBeInstanceOf(HTMLButtonElement);

			const submitButton = container.querySelector(".submit");
			expect(submitButton).toBeNull();

			const cnacelButton = container.querySelector(".cancel");
			expect(cnacelButton).toBeNull();
		}
	});

	it("Trigger onSubmit callback function", () => {
		const onSubmitMockFunc = jest.fn();

		act(() => {
			ReactDOM.render(
				<Form submitButton={true} cancelButton={true} onSubmit={onSubmitMockFunc} />,
				container
			);
		});

		if (!!container) {
			const submitButton = container.querySelector(".action-bar .submit");
			if (!!submitButton) {
				submitButton.dispatchEvent(new MouseEvent("click", { bubbles: true }));
				expect(onSubmitMockFunc).toHaveBeenCalled();
			}
		}
	});

	it("Trigger onCancel callback function", () => {
		const onCancelMockFunc = jest.fn();

		act(() => {
			ReactDOM.render(
				<Form submitButton={true} cancelButton={true} onCancel={onCancelMockFunc} />,
				container
			);
		});

		if (!!container) {
			const cancelButton = container.querySelector(".action-bar .cancel");
			if (!!cancelButton) {
				cancelButton.dispatchEvent(new MouseEvent("click", { bubbles: true }));
				expect(onCancelMockFunc).toHaveBeenCalled();
			}
		}
	});

	it("Display content inside form", () => {
		act(() => {
			const formContent = [
				<TextField model={{ field1: 1 }} modelProperty="field1" className="custom-input" key={1} />,
			];
			ReactDOM.render(
				<Form submitButton={true} cancelButton={true}>
					{formContent}
				</Form>,
				container
			);
		});

		if (!!container) {
			const customButton = container.querySelector(".crud__form-container .custom-input");
			expect(customButton).toBeInstanceOf(HTMLDivElement);
		}
	});
});
