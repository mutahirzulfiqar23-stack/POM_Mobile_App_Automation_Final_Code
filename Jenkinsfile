pipeline {
    agent any

    stages {

        stage('Clone Repository') {
            steps {
                git 'https://github.com/mutahirzulfiqar23-stack/POM_Mobile_App_Automation_Final_Code.git'
            }
        }

        stage('Restore Packages') {
            steps {
                bat 'dotnet restore POM_Mobile_App_Automate_Stage.slnx'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build POM_Mobile_App_Automate_Stage.slnx --configuration Release'
            }
        }

        stage('Run Tests') {
            steps {
                bat 'dotnet test POM_Mobile_App_Automate_Stage.slnx --logger "trx;LogFileName=test_results.trx"'
            }
        }

        stage('Publish Test Report') {
            steps {
                publishHTML([
                    reportDir: '.', 
                    reportFiles: 'test_results.trx', 
                    reportName: 'Test Report'
                ])
            }
        }
    }
}