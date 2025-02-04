﻿namespace MountainGoapLogging {
    using MountainGoap;
    using Serilog;
    using Serilog.Core;

    public class DefaultLogger {
        private readonly Logger logger;
        
        public DefaultLogger(bool logToConsole = true, string? loggingFile = null) {
            var config = new LoggerConfiguration();
            if (logToConsole) config.WriteTo.Console();
            if (loggingFile != null) config.WriteTo.File(loggingFile);
            Agent.OnAgentActionSequenceCompleted += OnAgentActionSequenceCompleted;
            Agent.OnAgentStep += OnAgentStep;
            Agent.OnPlanningStarted += OnPlanningStarted;
            Agent.OnPlanningFinished += OnPlanningFinished;
            Agent.OnPlanningFinishedForSingleGoal += OnPlanningFinishedForSingleGoal;
            Action.OnBeginExecuteAction += OnBeginExecuteAction;
            Action.OnFinishExecuteAction += OnFinishExecuteAction;
            Sensor.OnSensorRun += OnSensorRun;
            logger = config.CreateLogger();
        }

        private void OnAgentActionSequenceCompleted(Agent agent) {
            logger.Information("Agent {agent} completed action sequence.", agent.Name);
        }

        private void OnAgentStep(Agent agent) {
            logger.Information("Agent {agent} is working.", agent.Name);
        }

        private void OnBeginExecuteAction(Agent agent, Action action, Dictionary<string, object> parameters) {
            logger.Information("Agent {agent} began executing action {action}.", agent.Name, action.Name);
            if (parameters.Count == 0) return;
            logger.Information("\tAction parameters:");
            foreach (var kvp in parameters) logger.Information("\t\t{key}: {value}", kvp.Key, kvp.Value);
        }

        private void OnFinishExecuteAction(Agent agent, Action action, ExecutionStatus status, Dictionary<string, object> parameters) {
            logger.Information("Agent {agent} finished executing action {action} with status {status}.", agent.Name, action.Name, status);
        }

        private void OnPlanningFinished(Agent agent, BaseGoal? goal, float utility) {
            if (goal is null) logger.Warning("Agent {agent} finished planning and found no possible goal.", agent.Name);
            else logger.Information("Agent {agent} finished planning with goal {goal}, utility value {utility}.", agent.Name, goal.Name, utility);
        }

        private void OnPlanningFinishedForSingleGoal(Agent agent, BaseGoal goal, float utility) {
            logger.Information("Agent {agent} finished planning for goal {goal}, utility value {utility}.", agent.Name, goal.Name, utility);
        }

        private void OnPlanningStarted(Agent agent) {
            logger.Information("Agent {agent} started planning.", agent.Name);
        }

        private void OnSensorRun(Agent agent, Sensor sensor) {
            logger.Information("Agent {agent} ran sensor {sensor}.", agent.Name, sensor.Name);
        }
    }
}
