pipeline {
    agent any

    tools {
        allure 'Allure'
    }

    environment {
        DOTNET_PATH = "C:/Program Files/dotnet/dotnet.exe"
        ALLURE_RESULTS = "allure-results"
        CSPROJ_PATH = "C:/Users/Mutahir/source/repos/POM_Mobile_App_Automate_Stage/POM_Mobile_App_Automate_Stage.csproj"
    }

    stages {

        stage('Checkout Code') {
            steps {
                git 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat "\"${DOTNET_PATH}\" restore \"${CSPROJ_PATH}\""
            }
        }

        stage('Build Project') {
            steps {
                bat "\"${DOTNET_PATH}\" build \"${CSPROJ_PATH}\""
            }
        }

        stage('Run Automation Tests') {
            steps {
                bat "\"${DOTNET_PATH}\" test \"${CSPROJ_PATH}\" --filter FullyQualifiedName~SanityMain --logger trx --results-directory ${ALLURE_RESULTS}"
            }
        }

    }

    post {
        always {
            allure includeProperties: false, jdk: '', results: [[path: "${ALLURE_RESULTS}"]]
        }
    }
}