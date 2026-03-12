pipeline {
    agent any
    tools {
        allure 'Allure'
    }
    environment {
        ALLURE_RESULTS = "allure-results"
        ANDROID_HOME   = "C:\\Android\\sdk"
        PATH           = "${env.PATH};C:\\Android\\sdk\\platform-tools"
    }
    stages {

        stage('Clean Workspace') {
            steps {
                cleanWs()
            }
        }

        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
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

        stage('Verify ADB') {
            steps {
                bat 'adb version'
            }
        }

        stage('Run Automation Tests') {
            steps {
                bat 'dotnet test --no-build --logger trx --results-directory allure-results --filter "FullyQualifiedName~SanityMain"'
            }
        }
    }
    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: 'allure-results']]
        }
    }
}