import { withStyles } from "@material-ui/styles";
import { TextField, Button, FormControl } from "@material-ui/core";

export const LoginTextField = withStyles({
	root: {
		"& .MuiOutlinedInput-root": {
			borderRadius: 100,
			boxShadow:
				"-1px 4px 2px -2px rgba(113,210,245,0.3), -1px 2px 2px 0px rgba(113,210,245, 0.14), 0px 1px 5px 0px rgba(113,210,245,0.12)",

			"& fieldset": {
				transition: "box-shadow 0.3s",
				borderColor: "transparent",
			},
			"&:hover fieldset": {
				borderColor: "transparent",
				boxShadow:
					"-1px 3px 6px -1px rgba(113,210,245,0.3), -1px 4px 5px 0px rgba(113,210,245,0.14), 0px 1px 10px 0px rgba(113,210,245,0.12)",
			},
			"&.Mui-focused fieldset": {
				borderColor: "transparent",
				boxShadow:
					"-1px 3px 6px -1px rgba(113,210,245,0.3), -1px 4px 5px 0px rgba(113,210,245,0.14), 0px 1px 10px 0px rgba(113,210,245,0.12)",
			},
		},
		"& .Mui-error": {
			"& fieldset": {
				transition: "box-shadow 0.3s",
				borderColor: "transparent !important",
				boxShadow:
					"-1px 4px 2px -2px rgba(252,107,122,0.4), -1px 2px 2px 0px rgba(252,107,122, 0.3), 0px 1px 5px 0px rgba(252,107,122,0.2)",
			},
		},
	},
})(TextField);

export const LoginButton = withStyles({
	root: {
		background: "linear-gradient(90deg, rgba(113, 210, 245, 1) 0%, rgba(146, 243, 230, 1) 100%)",
		borderRadius: 100,
		height: "48px",
		width: "12rem",
		color: "white",
		fontSize: "1rem",
		fontFamily: "'Poppins', sans-serif",
		boxShadow:
			"-2px 4px 4px -2px rgba(113,210,245,0.5), -1px 2px 2px 0px rgba(113,210,245, 0.14), 0px 1px 5px 0px rgba(113,210,245,0.12)",
		"&:hover": {
			boxShadow:
				"-2px 5px 8px -1px rgba(113,210,245,0.5), -1px 4px 5px 0px rgba(113,210,245,0.14), 0px 1px 10px 0px rgba(113,210,245,0.12)",
		},
	},
})(Button);

export const FloatingNavigation = withStyles({
	root: {
		background: "linear-gradient(90deg, rgba(113, 210, 245, 1) 0%, rgba(146, 243, 230, 1) 100%)",
		borderRadius: 100,
		color: "white",
		height: "4rem",
		width: "4rem",
		position: "absolute",
		bottom: "80px",
		left: "20px",
		zIndex: 100,
	},
})(Button);

export const BackButton = withStyles({
	root: {
		// background: 'linear-gradient(90deg, rgba(113, 210, 245, 1) 0%, rgba(146, 243, 230, 1) 100%)',
		borderRadius: "1rem",
		color: "grey",
		height: "3rem",
		width: "15rem",
		textTransform: "none",
		fontFamily: "'Poppins', sans-serif",
		// '&:hover': {
		// 	boxShadow: '-2px 4px 4px -2px rgba(113,210,245,0.5), -1px 2px 2px 0px rgba(113,210,245, 0.14), 0px 1px 5px 0px rgba(113,210,245,0.12)',
		// },
	},
})(Button);

export const BackendButton = withStyles({
	root: {
		borderRadius: "1rem",
		margin: "1rem",
		background: "#1c1e26",
		color: "#ffffff",
		fontSize: "1.2rem",
		height: "10rem",
		width: "15rem",
		textTransform: "none",
		fontFamily: "'Poppins', sans-serif",
		transition: "all 0.3s",
		"&:hover": {
			background: "#1c1e26",
			transform: "scale(1.1)",
		},
	},
})(Button);

export const QuickLinkButton = withStyles({
	root: {
		background: "#1c1e26",
		borderRadius: "1rem",
		height: "10rem",
		width: "15rem",
		margin: "1rem",
		color: "white",
		fontSize: "1rem",
		fontFamily: "'Poppins', sans-serif",
		boxShadow: "2px 4px 4px -2px #dddddd",
		transition: "all 0.3s",
		"&:hover": {
			background: "#1c1e26",
			boxShadow: "2px 5px 8px -1px #dddddd",
			transform: "scale(1.05)",
		},
	},
})(Button);

// export const HomeSelect = withStyles({
// 	root: { minWidth: '20rem' },
// })(FormControl);

// export const LinkButton = withStyles({
// 	root: {
// 		// background: 'linear-gradient(90deg, rgba(113, 210, 245, 1) 0%, rgba(146, 243, 230, 1) 100%)',
// 		fontSize: '1rem',
// 		fontFamily: "'Poppins', sans-serif",
// 		textTransform: 'none',
// 	},
// })(Button);

// export const ActiveLinkButton = withStyles({
// 	root: {
// 		background: 'linear-gradient(90deg, rgba(113, 210, 245, 1) 0%, rgba(146, 243, 230, 1) 100%)',
// 		fontSize: '1rem',
// 		fontFamily: "'Poppins', sans-serif",
// 		textTransform: 'none',
// 		// '& MuiSvgIcon-root': {
// 		// 	color: 'white',
// 		// },
// 	},
// })(Button);
