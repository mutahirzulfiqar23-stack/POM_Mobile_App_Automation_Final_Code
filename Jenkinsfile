pipeline {
    agent any

    tools {
        allure 'Allure'
    }

    environment {
        DOTNET_PATH = "C:/Program Files/dotnet/dotnet.exe"
        ALLURE_RESULTS = "allure-results"
        PROJECT = "POM_Mobile_App_Automate_Stage.csproj"
    }

    stages {

        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
            }
        }

        stage('Reset Workspace') {
            steps {
                bat 'git reset --hard'
                bat 'git clean -fd'
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat "\"${DOTNET_PATH}\" restore ${PROJECT}"
            }
        }

        stage('Build Project') {
            steps {
                bat "\"${DOTNET_PATH}\" build ${PROJECT} --no-restore"
            }
        }

        stage('Run Automation Tests') {
            steps {
                bat "\"${DOTNET_PATH}\" test ${PROJECT} --no-build --filter FullyQualifiedName~SanityMain --logger trx --results-directory ${ALLURE_RESULTS}"
            }
        }

    }

    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: "${ALLURE_RESULTS}"]]
        }
    }
}