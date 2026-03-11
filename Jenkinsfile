pipeline {
    agent any

    tools {
        allure 'Allure'
    }

    environment {
        ALLURE_RESULTS = "allure-results"
    }

    stages {

        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
            }
        }

        stage('Clean Workspace') {
            steps {
                // Clean all temp files safely
                bat 'git reset --hard'
                bat 'git clean -fdx'
                bat 'dotnet nuget locals all --clear'
            }
        }

        stage('Restore NuGet Packages') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build Project') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Run Automation Tests') {
            steps {
                bat 'dotnet test --no-build --logger trx --results-directory allure-results'
            }
        }

    }

    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: 'allure-results']]
        }
    }
}