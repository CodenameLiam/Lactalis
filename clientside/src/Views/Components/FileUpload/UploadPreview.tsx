import * as React from "react";
import classNames from "classnames";
import { observer } from "mobx-react";
import { action, computed, observable } from "mobx";
import { Button, Display } from "Views/Components/Button/Button";
import If from "Views/Components/If/If";

export interface PreviewProps {
	/**
	 * The file to preview.
	 */
	fileName?: string;
	/**
	 * Should the preview show an image.
	 */
	fileUrl?: string;
	/**
	 * Callback to delete this file. If this function is not defined then this function will not be displayed.
	 */
	onDelete?: () => void;
	/**
	 * Is the preview of an image
	 */
	imagePreview?: boolean;
	/**
	 * Should name of this file be a link to the url
	 */
	download?: boolean;
}

/**
 * Preview display for the file upload component
 */
@observer
export class UploadPreview extends React.Component<PreviewProps> {
	@computed
	ed get className() {
		return classNames(this.props.imagePreview ? "upload__image" : "upload__file", "preview");
	}

	public render() {
		return (
			<div className={this.className}>
				{this.props.imagePreview ? (
					<ImagePreview {...this.props} />
				) : (
					<FilePreview {...this.props} />
				)}
			</div>
		);
	}
}

export interface FilePreviewProps extends Omit<PreviewProps, "fileUrl"> {
	/**
	 * The file to accept
	 */
	fileBlob: Blob;
}

/**
 * Preview for the file upload that takes a base64 file instead of a URL
 */
@observer
export class FileUploadPreview extends React.Component<FilePreviewProps> {
	@observable
	ed base64File?: string = undefined;

	@action
	ed onImageLoaded = (event: ProgressEvent<FileReader>) => {
		const result = event.target?.result;
		if (typeof result === "string") {
			this.base64File = result;
		}
	};

	ed loadFile = (file: Blob) => {
		const reader = new FileReader();
		reader.onload = this.onImageLoaded;
		reader.readAsDataURL(file);
	};

	public componentDidMount() {
		this.loadFile(this.props.fileBlob);
	}

	public componentDidUpdate(prevProps: Readonly<FilePreviewProps>) {
		if (this.props.fileBlob !== prevProps.fileBlob) {
			this.loadFile(this.props.fileBlob);
		}
	}

	public render() {
		if (!this.base64File) {
			return null;
		}

		const { fileName, onDelete, imagePreview } = this.props;

		return (
			<UploadPreview
				fileName={fileName}
				onDelete={onDelete}
				imagePreview={imagePreview}
				fileUrl={this.base64File}
			/>
		);
	}
}

const FileName = ({ fileUrl, fileName, download }: PreviewProps) => {
	if (download && fileUrl) {
		return (
			<a
				className="file-name icon-download icon-right"
				target="_blank"
				rel="noopener noreferrer"
				href={fileUrl}>
				{fileName}
			</a>
		);
	}
	return <p className="file-name">{fileName}</p>;
};

const ImagePreview = (props: PreviewProps) => (
	<div className="image">
		<img src={props.fileUrl} alt={props.fileName} />
		<FileName {...props} />
		<If condition={props.onDelete !== undefined}>
			<Button
				onClick={props.onDelete}
				display={Display.Outline}
				icon={{ icon: "bin-delete", iconPos: "icon-left" }}></Button>
		</If>
	</div>
);

const FilePreview = (props: PreviewProps) => (
	<div className="file">
		<FileName {...props} />
		<If condition={props.onDelete !== undefined}>
			<Button
				onClick={props.onDelete}
				display={Display.Outline}
				icon={{ icon: "bin-delete", iconPos: "icon-left" }}></Button>
		</If>
	</div>
);
