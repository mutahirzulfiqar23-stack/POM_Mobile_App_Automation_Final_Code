pipeline {
    agent any

    tools {
        allure 'Allure'
    }

    environment {
        ALLURE_RESULTS = "allure-results"
        CSPROJ_PATH = "C:/Users/Mutahir/source/repos/POM_Mobile_App_Automate_Stage/POM_Mobile_App_Automate_Stage.csproj"
    }

    stages {

        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
            }
        }

        stage('Clean Workspace') {
            steps {
                bat 'git reset --hard'
                bat 'git clean -fdx'
            }
        }

        stage('Restore NuGet Packages') {
            steps {
                bat 'dotnet nuget locals all --clear'
                bat "dotnet restore \"${CSPROJ_PATH}\""
            }
        }

        stage('Build Project') {
            steps {
                bat "dotnet build \"${CSPROJ_PATH}\" --no-restore"
            }
        }

        stage('Run Automation Tests') {
            steps {
                bat "dotnet test \"${CSPROJ_PATH}\" --no-build --logger trx --results-directory ${ALLURE_RESULTS}"
            }
        }

    }

    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: "${ALLURE_RESULTS}"]]
        }
    }
}