module.exports = function(grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        jshint: {
            files: ['app/**/*.js', 'assets/js/**/*.js'],
            options: {
                globals: {
                    jQuery: true
                }
            }
        },
        watch: {
            files: ['<%= jshint.files %>'],
            tasks: ['jshint']
        },
        less: {
            development: {
                options: {
                    paths: ['assets/libs/bootstrap/less']
                },
                files: {
                    'assets/css/bootstrap-override.css': 'assets/css/bootstrap-override.less'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-less');

    grunt.registerTask('default', ['jshint', 'less:development']);

};