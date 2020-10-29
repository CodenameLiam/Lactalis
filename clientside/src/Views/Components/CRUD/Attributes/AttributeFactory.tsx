import * as React from "react";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { Model } from "Models/Model";
import AttributeTextField from "./AttributeTextField";
import AttributeTextArea from "./AttributeTextArea";
import AttributeReferenceCombobox from "./AttributeReferenceCombobox";
import AttributeDatePicker from "./AttributeDatePicker";
import AttributeTimePicker from "./AttributeTimePicker";
import AttributeCheckbox from "./AttributeCheckbox";
import AttributePassword from "./AttributePassword";
import AttributeDisplayField from "./AttributeDisplayField";
import AttributeReferenceMultiCombobox from "./AttributeReferenceMultiCombobox";
import AttributeDateTimePicker from "./AttributeDateTimePicker";
import AttributeEnumCombobox from "./AttributeEnumCombobox";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import AttributeFile from "Views/Components/CRUD/Attributes/AttributeFile";

export function getAttributeComponent(
	attributeOptions: AttributeCRUDOptions,
	model: Model,
	errors: string[],
	formMode: EntityFormMode = EntityFormMode.VIEW,
	isRequired: boolean = false,
	onAfterChange?: (attributeName: string) => void,
	onChangeAndBlur?: (attributeName: string) => void
) {
	const className = attributeOptions.className
		? `${attributeOptions.attributeName} ${attributeOptions.className}`
		: attributeOptions.attributeName;

	const isReadonly = formMode === EntityFormMode.VIEW || attributeOptions.isReadonly;

	const displayType = {
		[EntityFormMode.VIEW]: attributeOptions.readFieldType,
		[EntityFormMode.CREATE]: attributeOptions.createFieldType,
		[EntityFormMode.EDIT]: attributeOptions.updateFieldType,
	}[formMode];

	switch (displayType) {
		case "textfield":
			return (
				<AttributeTextField
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					errors={errors}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onAfterChange) {
							onAfterChange(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					onChangeAndBlur={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "textarea":
			return (
				<AttributeTextArea
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					errors={errors}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onAfterChange) {
							onAfterChange(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					onChangeAndBlur={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "password":
			return (
				<AttributePassword
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					errors={errors}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "datepicker":
			return (
				<AttributeDatePicker
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "timepicker":
			return (
				<AttributeTimePicker
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "datetimepicker":
			return (
				<AttributeDateTimePicker
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "checkbox":
			return (
				<AttributeCheckbox
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "displayfield":
			return (
				<AttributeDisplayField
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					errors={errors}
					className={className}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "reference-combobox":
			if (attributeOptions.referenceTypeFunc === undefined) {
				throw new Error(
					"Must have a defined referenceType for display type" + attributeOptions.displayType
				);
			}
			return (
				<AttributeReferenceCombobox
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					referenceType={attributeOptions.referenceTypeFunc()}
					errors={errors}
					optionEqualFunc={attributeOptions.optionEqualFunc}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					fetchReferenceEntity={attributeOptions.isJoinEntity}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "reference-multicombobox":
			if (attributeOptions.referenceTypeFunc === undefined) {
				throw new Error(
					"Must have a defined referenceType for display type" + attributeOptions.displayType
				);
			}

			return (
				<AttributeReferenceMultiCombobox
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					referenceType={attributeOptions.referenceTypeFunc()}
					referenceResolveFunction={attributeOptions.referenceResolveFunction}
					optionEqualFunc={attributeOptions.optionEqualFunc}
					errors={errors}
					isJoinEntity={attributeOptions.isJoinEntity}
					disableDefaultOptionRemoval={attributeOptions.disableDefaultOptionRemoval}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "enum-combobox":
			if (attributeOptions.enumResolveFunction === undefined) {
				throw new Error(
					"Must have a defined enumType for display type" + attributeOptions.displayType
				);
			}
			return (
				<AttributeEnumCombobox
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					errors={errors}
					className={className}
					isReadonly={isReadonly}
					isRequired={isRequired}
					formMode={formMode}
					onAfterChange={() => {
						if (!!onChangeAndBlur) {
							onChangeAndBlur(attributeOptions.attributeName);
						}
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
				/>
			);
		case "file":
			if (attributeOptions.fileAttribute === undefined) {
				throw new Error(`Must have a defined file attribute for ${attributeOptions.attributeName}`);
			}
			return (
				<AttributeFile
					key={attributeOptions.attributeName}
					model={model}
					options={attributeOptions}
					className={className}
					isReadonly={isReadonly}
					errors={errors}
					isRequired={isRequired}
					fileAttribute={attributeOptions.fileAttribute}
					formMode={formMode}
					onAfterChange={() => {
						if (attributeOptions.onAfterChange) {
							attributeOptions.onAfterChange(model);
						}
					}}
					{...attributeOptions.inputProps}
				/>
			);
		case "hidden":
			return null;
		default:
			throw new Error(
				`No attribute component is defined to handle ${attributeOptions.displayType} for attr ${attributeOptions.attributeName}`
			);
	}
}
