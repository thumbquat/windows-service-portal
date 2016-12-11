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
                plugins: ["transform-react-jsx"],
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
        }
    });
    grunt.registerTask("all", ["clean", "babel"]);
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-babel");
    grunt.loadNpmTasks("grunt-contrib-watch");
};