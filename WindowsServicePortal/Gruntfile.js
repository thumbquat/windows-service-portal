/// <binding ProjectOpened='watch' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
	grunt.initConfig({
		clean: {
			contents: ["wwwroot/js/*"]
		},
		babel: {
			options: {
				plugins: ["transform-react-jsx", "transform-es2015-modules-commonjs"],
				presets: ["es2015", "react"]
			},
			jsx: {
				files: [{
					expand: true,
					cwd: "jssrc/",
					src: ["*.jsx"],
					dest: "wwwroot/js/",
					ext: ".js"
				}]
			}
		},
		watch: {
			files: ["jssrc/*"],
			tasks: ["all"]
		},
		copy: {
			main: {
				files: [{
					flatten: true,
					expand: true,
					src: [
						"node_modules/react/dist/react.js",
						"node_modules/react-dom/dist/react-dom.js",
						"node_modules/redux/dist/redux.js",
						"node_modules/react-redux/dist/react-redux.js"
						],
					dest: "wwwroot/js/",
					filter: "isFile"
				}]
			}
		}
	});
	grunt.registerTask("all", ["clean", "babel", "copy"]);
	grunt.loadNpmTasks("grunt-contrib-copy");
	grunt.loadNpmTasks("grunt-contrib-clean");
	grunt.loadNpmTasks("grunt-babel");
	grunt.loadNpmTasks("grunt-contrib-watch");
};